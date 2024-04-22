using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.Controllers;

public class OrderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}