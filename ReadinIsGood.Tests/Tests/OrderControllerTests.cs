using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadingIsGood.Controllers;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.RequestModels;


namespace ReadingIsGood.Tests;

public class OrderControllerTests
{
    [Fact]
    public void CreateOrder_ReturnsOkResult()
    {
        // Arrange
        var mockOrderService = new Mock<IOrderService>();
        mockOrderService.Setup(service => service.CreateOrder(It.IsAny<CreateOrderRequestModel>(), It.IsAny<int>()))
            .Returns(true);
        var controller = new OrderController(mockOrderService.Object);
        List<BookCount> books = new();
        books.Add(new BookCount { Count = 7, BookId = 1 });
        var createorderReq = new CreateOrderRequestModel() { Books = books };
        // Act
        var result = controller.CreateOrder(createorderReq);
        Console.WriteLine(result);
        // Assert
        // Assert.IsType<OkObjectResult>(result);
    }

    // [Fact]
    // public void GetOrdersByDate_WithValidDates_ReturnsOkResult()
    // {
    //     // Arrange
    //     var mockOrderService = new Mock<OrderService>();
    //     var controller = new OrderController(mockOrderService.Object);
    //
    //     DateTime startDate = new DateTime(2023, 1, 1);
    //     DateTime endDate = new DateTime(2023, 6, 30);
    //
    //     var orders = new List<OrderVM>
    //     {
    //         new OrderVM { Id = 1, OrderDate = DateTime.Now, TotalAmount = 100 },
    //         new OrderVM { Id = 2, OrderDate = DateTime.Now, TotalAmount = 150 }
    //     };
    //
    //     mockOrderService.Setup(service => service.GetOrders(startDate, endDate)).Returns(orders);
    //
    //     // Act
    //     var result = controller.GetOrdersByDate(startDate, endDate) as OkObjectResult;
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(200, result.StatusCode);
    //     var responseModel = result.Value as ResponseModel<List<OrderVM>>;
    //     Assert.NotNull(responseModel);
    //     Assert.True(responseModel.Success);
    //     Assert.Equal(2, responseModel.Data.Count);
    // }
    //
    // [Fact]
    // public void GetOrdersByDate_WithEndDateGreaterThanStartDate_ReturnsBadRequestResult()
    // {
    //     // Arrange
    //     var mockOrderService = new OrderService();
    //     var controller = new OrderController(mockOrderService);
    //
    //     DateTime startDate = new DateTime(2024, 3, 1);
    //     DateTime endDate = new DateTime(2024, 3, 20);
    //
    //     // Act
    //     var result = controller.GetOrdersByDate(startDate, endDate) as OkObjectResult;
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(200, result.StatusCode);
    //     var responseModel = result.Value as ResponseModel<bool>;
    //     Assert.NotNull(responseModel);
    //     Assert.False(responseModel.Success);
    //     Assert.Equal("endDate cannot be greater than startDate!", responseModel.Message);
    // }

    // [Fact]
    // public void GetOrdersByDate_WithDateDifferenceGreaterThan180Days_ReturnsBadRequestResult()
    // {
    //     // Arrange
    //     var mockOrderService = new Mock<OrderService>();
    //     var controller = new OrderController(mockOrderService.Object);
    //
    //     DateTime startDate = new DateTime(2023, 1, 1);
    //     DateTime endDate = new DateTime(2023, 7, 1);
    //
    //     // Act
    //     var result = controller.GetOrdersByDate(startDate, endDate) as OkObjectResult;
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(200, result.StatusCode);
    //     var responseModel = result.Value as ResponseModel<bool>;
    //     Assert.NotNull(responseModel);
    //     Assert.False(responseModel.Success);
    //     Assert.Equal("Cannot be older than 6 months!", responseModel.Message);
    // }
}