using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadingIsGood.Controllers;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Tests.ControllerTests;

public class BookControllerTests
{
    [Fact]
    public void CreateBook_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var bookRequestModel = new BookRequestModel();
        var mockBookService = new Mock<IBookService>();
        mockBookService.Setup(x => x.CreateBook(bookRequestModel)).Returns(true);
        var controller = new BookController(mockBookService.Object);

        // Act
        var result = controller.CreateBook(bookRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<bool>>(okResult.Value);
        Assert.True(responseModel.Data);
    }

    [Fact]
    public void GetBooks_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var mockBookService = new Mock<IBookService>();
        mockBookService.Setup(x => x.GetBooks()).Returns(new List<BookVM>());
        var controller = new BookController(mockBookService.Object);

        // Act
        var result = controller.GetBooks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<List<BookVM>>>(okResult.Value);
        Assert.Empty(responseModel.Data);
    }

    [Fact]
    public void GetBookStock_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var bookId = 1;
        var mockBookService = new Mock<IBookService>();
        mockBookService.Setup(x => x.GetBookStock(bookId)).Returns(new BookStockResponseModel());
        var controller = new BookController(mockBookService.Object);

        // Act
        var result = controller.GetBookStock(bookId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<BookStockResponseModel>>(okResult.Value);
        Assert.NotNull(responseModel.Data);
    }

    [Fact]
    public void UpdateBookStock_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var bookStockRequestModel = new BookStockRequestModel();
        var mockBookService = new Mock<IBookService>();
        mockBookService.Setup(x => x.UpdateBookStock(bookStockRequestModel)).Returns(new BookStockResponseModel());
        var controller = new BookController(mockBookService.Object);

        // Act
        var result = controller.UpdateBookStock(bookStockRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<BookStockResponseModel>>(okResult.Value);
        Assert.NotNull(responseModel.Data);
    }
}