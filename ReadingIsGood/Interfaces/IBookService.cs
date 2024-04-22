using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Interfaces;

public interface IBookService
{
    bool CreateBook(BookRequestModel bookRequest);
    List<BookVM> GetBooks();
    BookStockResponseModel GetBookStock(int bookId);
    BookStockResponseModel UpdateBookStock(BookStockRequestModel bookStockRequestModel);
}