using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadingIsGood.Controllers;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Tests.ControllerTests;

public class OrderControllerTests
{
    [Fact]
    public void CreateOrder_ShouldReturnOkResultWithFalse()
    {
        // Arrange
        var bookCount = new List<BookCount>();
        var createOrderRequestModel = new CreateOrderRequestModel { Books = bookCount };
        var mockOrderService = new Mock<IOrderService>();
        mockOrderService.Setup(x => x.CreateOrder(createOrderRequestModel, It.IsAny<int>())).Returns(true);
        var controller = new OrderController(mockOrderService.Object);

        // Act
        var result = controller.CreateOrder(createOrderRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<bool>>(okResult.Value);
        Assert.False(responseModel.Data);
    }


    [Fact]
    public void GetOrdersByDate_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var startDate = DateTime.UtcNow.AddDays(-7);
        var endDate = DateTime.UtcNow;
        var mockOrderService = new Mock<IOrderService>();
        mockOrderService.Setup(x => x.GetOrders(startDate, endDate)).Returns(new List<OrderVM>());
        var controller = new OrderController(mockOrderService.Object);

        // Act
        var result = controller.GetOrdersByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<List<OrderVM>>>(okResult.Value);
        Assert.NotNull(responseModel.Data);
        Assert.Empty(responseModel.Data);
    }
}