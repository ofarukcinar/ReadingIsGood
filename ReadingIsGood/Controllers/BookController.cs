using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    ///     Creates a new book.
    /// </summary>
    /// <param name="bookRequestModel">The details of the book to create.</param>
    /// <returns>True if the book is created successfully, otherwise false.</returns>
    [Authorize]
    [Route("api/book")]
    [HttpPost]
    public IActionResult CreateBook(BookRequestModel bookRequestModel)
    {
        var response = _bookService.CreateBook(bookRequestModel);
        return Ok(new ResponseModel<bool>(response));
    }

    /// <summary>
    ///     Retrieves all books.
    /// </summary>
    /// <returns>A list of books.</returns>
    [Authorize]
    [Route("api/books")]
    [HttpGet]
    public IActionResult GetBooks()
    {
        var response = _bookService.GetBooks();
        return Ok(new ResponseModel<List<BookVM>>(response));
    }

    /// <summary>
    ///     Retrieves the stock information of a book.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>The stock information of the book.</returns>
    [Authorize]
    [Route("api/books/{id:int}/stock")]
    [HttpGet]
    public IActionResult GetBookStock(int id)
    {
        var response = _bookService.GetBookStock(id);
        return Ok(new ResponseModel<BookStockResponseModel>(response));
    }

    /// <summary>
    ///     Updates the stock information of a book.
    /// </summary>
    /// <param name="bookRequestModel">The details of the book stock to update.</param>
    /// <returns>The updated stock information of the book.</returns>
    [Authorize]
    [Route("api/books/stock")]
    [HttpPut]
    public IActionResult UpdateBookStock(BookStockRequestModel bookRequestModel)
    {
        var response = _bookService.UpdateBookStock(bookRequestModel);
        return Ok(new ResponseModel<BookStockResponseModel>(response));
    }
}