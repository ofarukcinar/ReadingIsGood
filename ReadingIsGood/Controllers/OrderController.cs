using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.RequestModels;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;

namespace ReadingIsGood.Controllers;

public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    ///     Creates a new order.
    /// </summary>
    /// <param name="createOrderRequestModel">The details of the order to create.</param>
    /// <returns>The response containing the result of the operation.</returns>
    [Authorize]
    [Route("api/order")]
    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequestModel createOrderRequestModel)
    {
        if (!createOrderRequestModel.Books.Any())
            return Ok(new ResponseModel<bool>(false, "Book not found!"));

        var customerId = GetCustomerId();
        var response = _orderService.CreateOrder(createOrderRequestModel, customerId);
        return Ok(new ResponseModel<bool>(response));
    }

    /// <summary>
    ///     Retrieves orders between the specified dates.
    /// </summary>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>The response containing the orders within the specified date range.</returns>
    [Authorize]
    [Route("api/orders/by-date")]
    [HttpGet]
    public IActionResult GetOrdersByDate(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            return Ok(new ResponseModel<bool>(false, "endDate cannot be greater than startdate!"));

        var difference = (endDate - startDate).Duration();

        if (difference.TotalDays > 180)
            return Ok(new ResponseModel<bool>(false, "Cannot be older than 6 months!"));

        var response = _orderService.GetOrders(startDate, endDate);
        return Ok(new ResponseModel<List<OrderVM>>(response));
    }
}