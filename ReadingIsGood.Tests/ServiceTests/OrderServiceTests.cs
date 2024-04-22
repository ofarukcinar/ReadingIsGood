using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Tests.ServiceTests;

public class OrderServiceTests : IDisposable
{
    private readonly ReadingIsGoodContext _context;
    private readonly DbContextOptions<ReadingIsGoodContext> _dbContextOptions;

    public OrderServiceTests()
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
    public void GetOrders_ShouldReturnListOfOrderViewModels()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-60);
        var endDate = DateTime.UtcNow;
        var orderService = new OrderService(_context);

        // Act
        var result = orderService.GetOrders(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void CreateOrder_ShouldCreateNewOrderWithGivenBooks()
    {
        // Arrange
        var customerId = 1;
        var createOrderRequestModel = new CreateOrderRequestModel
        {
            Books = new List<BookCount>
            {
                new() { BookId = 1, Count = 2 },
                new() { BookId = 2, Count = 1 }
            }
        };
        var orderService = new OrderService(_context);

        // Act
        var result = orderService.CreateOrder(createOrderRequestModel, customerId);

        // Assert
        Assert.True(result);
        Assert.Equal(5, _context.Orders.Count());
        Assert.Equal(9, _context.Stocks.Count(s => s.Sold && s.OrderId != null));
    }
}