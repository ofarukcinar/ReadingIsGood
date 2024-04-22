using Mapster;
using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly ReadingIsGoodContext _db;
    private readonly object _lockObject = new();

    public OrderService(ReadingIsGoodContext dbContext)
    {
        _db = dbContext;
    }

    public List<OrderVM> GetOrders(DateTime startDate, DateTime endDate)
    {
        var orders = _db.Orders
            .Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate)
            .Include(o => o.Stocks)
            .ThenInclude(s => s.Book)
            .OrderBy(x => x.Id)
            .ToList();
        var orderVMs = orders.Adapt<List<OrderVM>>();
        SaveLog("GetOrders");
        return orderVMs;
    }

    public bool CreateOrder(CreateOrderRequestModel createOrderRequestModel, int customerId)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                lock (_lockObject)
                {
                    var order = new Order
                    {
                        CustomerId = customerId,
                        OrderDate = DateTime.UtcNow
                    };

                    _db.Orders.Add(order);
                    _db.SaveChanges();

                    foreach (var book in createOrderRequestModel.Books)
                        if (!ProcessBookOrder(book, order))
                        {
                            transaction.Rollback();
                            return false;
                        }

                    _db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                SaveErrorWithData("Order could not be created", createOrderRequestModel);
                throw new InvalidOperationException("order could not be created");
            }
        }
    }

    private bool ProcessBookOrder(BookCount bookOrderModel, Order order)
    {
        var stocks = _db.Stocks.Where(x => !x.Sold && x.BookId == bookOrderModel.BookId).ToList();

        if (stocks.Count < bookOrderModel.Count)
            return false;

        for (var i = 0; i < bookOrderModel.Count; i++)
        {
            stocks[i].Sold = true;
            stocks[i].OrderId = order.Id;
            stocks[i].SoldDate = DateTime.UtcNow;
        }

        return true;
    }
}