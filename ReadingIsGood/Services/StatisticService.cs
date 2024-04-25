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

    public List<StatisticResponseModel> GetMonthlyOrderStatistics(int customerId,DateTime startDate, DateTime endDate)
    {
        try
        {
            var statistics = _db.Orders
                .Where(x => x.CustomerId == customerId && x.OrderDate >= startDate && x.OrderDate <= endDate)
                .Include(o => o.Stocks)
                .ThenInclude(s => s.Book)
                .GroupBy(o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month })
                .Select(group => new StatisticResponseModel
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    TotalOrderCount = group.Count(),
                    TotalBookCount = group.Sum(o => o.Stocks.Count),
                    TotalPurchasedAmount = group.Sum(o => o.Stocks.Sum(s => s.Book.Price))
                })
                .OrderBy(s => s.Year).ThenBy(s=>s.Month)
                .ToList();

            return statistics;
        }

        catch (Exception ex)
        {
            throw new InvalidOperationException("Statistics not calculated!");
        }
    }
}