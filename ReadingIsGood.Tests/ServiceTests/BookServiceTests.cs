using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Tests.Tests;

public class BookServiceTests : IDisposable
{
    private readonly ReadingIsGoodContext _context;
    private readonly DbContextOptions<ReadingIsGoodContext> _dbContextOptions;

    public BookServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ReadingIsGoodContext>()
            .UseInMemoryDatabase("TestDb")
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
    public void UpdateBookStock_Increase_and_Decrease_ShouldUpdateStockCount()
    {
        // Arrange
        var bookId = 1;
        var updatedStockCount = 10;
        var bookStockRequestModel = new BookStockRequestModel { Id = bookId, Count = updatedStockCount };
        var bookService = new BookService(_context);

        // Act
        var result = bookService.UpdateBookStock(bookStockRequestModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedStockCount, result.Count);
    }

    [Fact]
    public void CreateBook_ShouldAddNewBookToDatabase()
    {
        // Arrange
        var bookRequest = new BookRequestModel { Title = "Test Book", Price = 13 };

        // Act
        var bookService = new BookService(_context);
        var result = bookService.CreateBook(bookRequest);

        // Assert
        Assert.True(result);
        Assert.Equal(5, _context.Books.Count());
        Assert.Contains(_context.Books, b => b.Title == "Test Book" && b.Price == 13);
    }

    [Fact]
    public void GetBooks_ShouldReturnListOfBookViewModels()
    {
        // Arrange
        var bookService = new BookService(_context);

        // Act
        var result = bookService.GetBooks();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 2, "Books count greater than 2");
    }

    [Fact]
    public void GetBookStock_ShouldReturnCorrectStockCount()
    {
        // Arrange
        var bookId = 1;
        var bookService = new BookService(_context);

        // Act
        var result = bookService.GetBookStock(bookId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count >= 0);
    }
}