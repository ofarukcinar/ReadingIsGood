using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.Controllers;

public class BaseController : ControllerBase
{
    [HttpGet]
    public int GetCustomerId()
    {
        return Convert.ToInt32(((ClaimsIdentity)HttpContext.User.Identity).FindFirst("id")?.Value);
    }
}