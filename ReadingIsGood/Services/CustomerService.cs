using Mapster;
using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Helper;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Services;

public class CustomerService : BaseService, ICustomerService
{
    private readonly ReadingIsGoodContext _db;

    public CustomerService(ReadingIsGoodContext dbContext)
    {
        _db = dbContext;
    }

    public ResponseModel<CustomerVM> CreateCustomer(CustomerRequestModel customerRequestModel)
    {
        if (GetCustomer(customerRequestModel) != null)
        {
            SaveErrorWithData("Email has been used.", customerRequestModel);
            return new ResponseModel<CustomerVM>(customerRequestModel.Adapt<CustomerVM>(), "Email has been used.");
        }

        var hasher = new Hasher(customerRequestModel.Password);
        var customer = customerRequestModel.Adapt<Customer>();
        customer.Password = Convert.ToBase64String(hasher.ToArray());
        _db.Customers.Add(customer);
        _db.SaveChanges();
        var customerVm = customer.Adapt<CustomerVM>();
        SaveLogWithData("Customer added.", customerVm);
        return new ResponseModel<CustomerVM>(customerVm);
    }

    public ResponseModel<List<OrderVM>> GetCustomerOrders(int customerId)
    {
        var orders = _db.Orders
            .Where(x => x.CustomerId == customerId)
            .Include(o => o.Stocks)
            .ThenInclude(s => s.Book)
            .ToList();
        var orderVMs = orders.Adapt<List<OrderVM>>();
        SaveLogWithData("User orders received.", customerId);
        return new ResponseModel<List<OrderVM>>(orderVMs);
    }

    public int ValidateUser(CustomerLoginRequestModel customerRequestModel)
    {
        var customer = GetCustomer(customerRequestModel.Adapt<CustomerRequestModel>());
        if (customer == null)
            return 0;
        var hasher = new Hasher(Convert.FromBase64String(customer.Password));
        if (customer?.Password != null && !hasher.Verify(customerRequestModel?.Password)) return 0;
        if (customer?.Id == 0)
        {
            SaveErrorWithData("Customer not validate!", customerRequestModel);
            throw new UnauthorizedAccessException();
        }

        return customer.Id;
    }

    private Customer? GetCustomer(CustomerRequestModel customerRequestModel)
    {
        var customer = _db.Customers.FirstOrDefault(x => x.Email == customerRequestModel.Email);
        return customer;
    }
}