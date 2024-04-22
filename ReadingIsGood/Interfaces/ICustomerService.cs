using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Interfaces;

public interface ICustomerService
{
    ResponseModel<CustomerVM> CreateCustomer(CustomerRequestModel customerRequestModel);
    ResponseModel<List<OrderVM>> GetCustomerOrders(int customerId);
    int ValidateUser(CustomerLoginRequestModel customerRequestModel);
}