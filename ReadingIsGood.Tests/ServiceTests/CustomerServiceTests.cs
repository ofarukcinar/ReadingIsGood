using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Tests.ServiceTests;

public class CustomerServiceTests : IDisposable
{
    private readonly ReadingIsGoodContext _context;
    private readonly DbContextOptions<ReadingIsGoodContext> _dbContextOptions;

    public CustomerServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ReadingIsGoodContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        _context = new ReadingIsGoodContext(_dbContextOptions);
        if (_context.Books.Any()) return;
        var initializer = new TestDataInitializer();
        initializer.SeedData(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void CreateCustomer_ShouldAddNewCustomerToDatabase()
    {
        // Arrange
        var customerRequest = new CustomerRequestModel
            { Email = "test@example.com", Password = "password", Name = "test" };

        // Act
        var customerService = new CustomerService(_context);
        var result = customerService.CreateCustomer(customerRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Equal(2, _context.Customers.Count());
        Assert.Contains(_context.Customers, c => c.Email == "test@example.com");
    }

    [Fact]
    public void GetCustomerOrders_ShouldReturnListOfOrderViewModelsForGivenCustomerId()
    {
        // Arrange
        var customerId = 1;
        var customerService = new CustomerService(_context);

        // Act
        var result = customerService.GetCustomerOrders(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.True(result.Data.Count > 1,
            "result.Data.Count greater than 1");
    }

    [Fact]
    public void ValidateUser_ShouldReturnCustomerIdForValidCustomerLoginRequest()
    {
        // Arrange
        var customerLoginRequest = new CustomerLoginRequestModel
            { Email = "john.doe@example.com", Password = "xayTZL21" };

        // Act
        var customerService = new CustomerService(_context);
        var result = customerService.ValidateUser(customerLoginRequest);

        // Assert
        Assert.NotEqual(0, result); // Assuming valid credentials
    }

    [Fact]
    public void ValidateUser_ShouldNotReturnCustomerIdForValidCustomerLoginRequest()
    {
        // Arrange
        var customerLoginRequest = new CustomerLoginRequestModel
            { Email = "test", Password = "xayTZL21" };

        // Act
        var customerService = new CustomerService(_context);
        var result = customerService.ValidateUser(customerLoginRequest);

        // Assert
        Assert.Equal(0, result); // Assuming valid credentials
    }
}