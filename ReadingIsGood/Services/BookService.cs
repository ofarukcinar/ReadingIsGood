using Mapster;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.DbModels;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Services;

public class BookService : BaseService, IBookService
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
            SaveLogWithData("Added new book.", book);
            return true;
        }
        catch (Exception ex)
        {
            SaveErrorWithData("Error occurred while saving book to database", ex.Message);
            throw new InvalidOperationException(ex.Message);
        }
    }

    public List<BookVM> GetBooks()
    {
        try
        {
            var books = _db.Books.ToList();
            var bookVms = books.Adapt<List<BookVM>>();
            SaveLog("Books Listed");
            return bookVms;
        }
        catch (Exception ex)
        {
            SaveErrorWithData("Error received while listing books", ex.Message);
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

            if (!stocks.Any())
            {
                SaveLogWithData("No stock available for the book.", bookId);
                return new BookStockResponseModel { Id = bookId, Count = 0 };
            }

            SaveLogWithData("Book stock received.", bookId);
            return new BookStockResponseModel { Id = stocks.FirstOrDefault().BookId, Count = stocks.Count };
        }
        catch (Exception ex)
        {
            SaveErrorWithData("Error received while book stock", ex.Message);
            throw new InvalidOperationException(ex.Message);
        }
    }

    public BookStockResponseModel UpdateBookStock(BookStockRequestModel bookStockRequestModel)
    {
        if (bookStockRequestModel == null || bookStockRequestModel.Count < 0)
        {
            SaveError("bookStockRequestModel cannot be null or the value cannot be less than zero ");
            throw new ArgumentNullException(nameof(bookStockRequestModel),
                "bookStockRequestModel cannot be null or the value cannot be less than zero");
        }

        var bookStock = GetBookStock(bookStockRequestModel.Id);
        if (bookStockRequestModel.Count == bookStock.Count)
        {
            SaveLogWithData("Book stocks equal.", bookStockRequestModel);
            return new BookStockResponseModel();
        }

        UpdateStocks(bookStock, bookStockRequestModel);

        var newBookStock = GetBookStock(bookStockRequestModel.Id);
        SaveLogWithData("Book stocks updated.", newBookStock);
        return newBookStock;
    }

    private void UpdateStocks(BookStockResponseModel bookStock, BookStockRequestModel bookStockRequestModel)
    {
        var book = _db.Books.FirstOrDefault(x => x.Id == bookStock.Id);
        if (book == null)
        {
            SaveErrorWithData("book not found", bookStockRequestModel);
            throw new InvalidOperationException($"Book with ID {bookStock.Id} not found");
        }

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
        SaveLogWithData("Books deleted.", stocksToDelete);
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

        SaveLogWithData("Books added.", stocks);
        _db.Stocks.AddRange(stocks);
        _db.SaveChanges();
    }
}