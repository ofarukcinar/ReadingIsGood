using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Models.ResponseModels;
using ReadingIsGood.Models.ViewModels;
using ReadingIsGood.Services;

namespace ReadingIsGood.Controllers;

public class StatisticController : BaseController
{
    private readonly StatisticService _statisticService;

    public StatisticController(StatisticService statisticService)
    {
        _statisticService = statisticService;
    }
    [Authorize]
    [Route("api/[controller]/[action]")]
    [HttpGet]
    public IActionResult GetMonthlyOrderStatistics()
    {
        var response = _statisticService.GetMonthlyOrderStatistics(GetCustomerId());
        return Ok(new ResponseModel<List<StatisticResponseModel>>(response));
    }}