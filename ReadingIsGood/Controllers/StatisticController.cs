using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Interfaces;
using ReadingIsGood.Models.ResponseModels;

namespace ReadingIsGood.Controllers;

public class StatisticController : BaseController
{
    private readonly IStatisticService _statisticService;

    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    /// <summary>
    ///     Retrieves monthly order statistics for the authenticated customer.
    /// </summary>
    /// <returns>The response containing monthly order statistics.</returns>
    [Authorize]
    [Route("api/order-montly-statistics")]
    [HttpGet]
    public IActionResult GetOrderMonthlyStatistics(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            return Ok(new ResponseModel<bool>(false, "endDate cannot be greater than Start Date!"));

        var response = _statisticService.GetMonthlyOrderStatistics(GetCustomerId(), startDate, endDate);
        return Ok(new ResponseModel<List<StatisticResponseModel>>(response));
    }
}