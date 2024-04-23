using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.ResponseModels;

namespace ReadingIsGood.Services;

public class StatisticService : IStatisticService
{
    private readonly ReadingIsGoodContext _db;

    public StatisticService(ReadingIsGoodContext db)
    {
        _db = db;
    }

    public List<StatisticResponseModel> GetMonthlyOrderStatistics(int customerId)
    {
        try
        {
            var now = DateTime.UtcNow;

            var lastMonthStartDate = now.AddMonths(-1).Date.AddDays(1 - now.Day);

            var lastMonthEndDate = now.Date.AddDays(-now.Day);

            var previousMonthStartDate = now.AddMonths(-2).Date.AddDays(1 - now.Day);

            var previousMonthEndDate = lastMonthStartDate.AddDays(-1);

            var twoMonthsAgoStartDate = now.AddMonths(-3).Date.AddDays(1 - now.Day);

            var twoMonthsAgoEndDate = previousMonthStartDate.AddDays(-1);

            var lastThreeMonthsOrders = _db.Orders
                .Where(x => (x.CustomerId == customerId &&
                             x.OrderDate >= twoMonthsAgoStartDate && x.OrderDate <= twoMonthsAgoEndDate) ||
                            (x.OrderDate >= previousMonthStartDate && x.OrderDate <= previousMonthEndDate) ||
                            (x.OrderDate >= lastMonthStartDate && x.OrderDate <= lastMonthEndDate))
                .Include(o => o.Stocks)
                .ThenInclude(s => s.Book)
                .ToList();

            var statistics = lastThreeMonthsOrders
                .GroupBy(o => new { Month = o.OrderDate.ToString("MMMM", CultureInfo.InvariantCulture) })
                .Select(group => new StatisticResponseModel
                {
                    Month = group.Key.Month,
                    TotalOrderCount = group.Count(),
                    TotalBookCount = group.Sum(o => o.Stocks.Select(s => s.BookId).Count()),
                    TotalPurchasedAmount = group.Sum(o => o.Stocks.Sum(s => s.Book.Price))
                })
                .OrderByDescending(s => s.Month)
                .ToList();
            return statistics;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Statistics not calculated!");
        }
    }
}