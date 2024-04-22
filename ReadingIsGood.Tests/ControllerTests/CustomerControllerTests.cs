using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadingIsGood.Controllers;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Tests.ControllerTests;

public class CustomerControllerTests
{
    [Fact]
    public void CreateCustomer_ShouldReturnOkResultWithResponseModel()
    {
        // Arrange
        var customerRequestModel = new CustomerRequestModel
            { Name = "test", Email = "name@name.com", Password = "nthsc21" };
        var mockCustomerService = new Mock<ICustomerService>();
        mockCustomerService.Setup(x => x.CreateCustomer(customerRequestModel)).Returns(
            new ResponseModel<CustomerVM>(new CustomerVM
                { Email = customerRequestModel.Email, Name = customerRequestModel.Name }));
        var controller = new CustomerController(mockCustomerService.Object, new AppSettings());

        // Act
        var result = controller.CreateCustomer(customerRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<CustomerVM>>(okResult.Value);
        Assert.NotNull(responseModel);
    }

    [Fact]
    public void Login_ShouldReturnOkResultWithResponseModelContainingToken()
    {
        // Arrange
        var customerLoginRequestModel = new CustomerLoginRequestModel { Password = "test", Email = "test" };
        var mockCustomerService = new Mock<ICustomerService>();
        mockCustomerService.Setup(x => x.ValidateUser(customerLoginRequestModel)).Returns(0);
        var controller = new CustomerController(mockCustomerService.Object, new AppSettings());

        // Act
        var result = controller.Login(customerLoginRequestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseModel = Assert.IsType<ResponseModel<string>>(okResult.Value);
        Assert.NotNull(responseModel);
    }
}