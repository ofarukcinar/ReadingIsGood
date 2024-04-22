using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Interfaces;

public interface IOrderService
{
    List<OrderVM> GetOrders(DateTime startDate, DateTime endDate);
    bool CreateOrder(CreateOrderRequestModel createOrderRequestModel, int customerId);
}