using Mapster;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Services;

public class BookService : IBookService
{
    private readonly ReadingIsGoodContext _db;

    public BookService(ReadingIsGoodContext dbContext)
    {
        _db = dbContext;
    }

    public bool CreateBook(BookRequestModel bookRequest)
    {
        try
        {
            var book = bookRequest.Adapt<Book>();
            _db.Books.Add(book);
            _db.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public List<BookVM> GetBooks()
    {
        try
        {
            var books = _db.Books.ToList();
            var bookVms = books.Adapt<List<BookVM>>();
            return bookVms;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public BookStockResponseModel GetBookStock(int bookId)
    {
        try
        {
            var stocks = _db.Stocks.Where(x => x.BookId == bookId && !x.Sold)
                .Select(x => new { x.Id, x.BookId })
                .ToList();

            if (!stocks.Any()) return new BookStockResponseModel { Id = bookId, Count = 0 };

            return new BookStockResponseModel { Id = stocks.FirstOrDefault().BookId, Count = stocks.Count };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public BookStockResponseModel UpdateBookStock(BookStockRequestModel bookStockRequestModel)
    {
        if (bookStockRequestModel == null || bookStockRequestModel.Count < 0)
            throw new ArgumentNullException(nameof(bookStockRequestModel),
                "bookStockRequestModel cannot be null or the value cannot be less than zero");

        var bookStock = GetBookStock(bookStockRequestModel.Id);
        if (bookStockRequestModel.Count == bookStock.Count) return new BookStockResponseModel();

        UpdateStocks(bookStock, bookStockRequestModel);

        var newBookStock = GetBookStock(bookStockRequestModel.Id);
        return newBookStock;
    }

    private void UpdateStocks(BookStockResponseModel bookStock, BookStockRequestModel bookStockRequestModel)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == bookStock.Id);
        if (book == null) throw new InvalidOperationException($"Book with ID {bookStock.Id} not found");

        if (bookStock.Count > bookStockRequestModel.Count)
            DeleteExcessStocks(book, bookStock.Count - bookStockRequestModel.Count);
        else
            AddStocks(book, bookStockRequestModel.Count - bookStock.Count);
    }

    private void DeleteExcessStocks(Book book, int countToDelete)
    {
        var stocksToDelete = _db.Stocks
            .Where(x => !x.Sold && x.OrderId == null && x.BookId == book.Id)
            .Take(countToDelete)
            .ToList();

        _db.Stocks.RemoveRange(stocksToDelete);
        _db.SaveChanges();
    }

    private void AddStocks(Book book, int countToAdd)
    {
        var stocks = new List<Stock>();
        for (var i = 0; i < countToAdd; i++)
        {
            var newStock = new Stock
            {
                BookId = book.Id,
                Sold = false
            };
            stocks.Add(newStock);
        }

        _db.Stocks.AddRange(stocks);
        _db.SaveChanges();
    }
}