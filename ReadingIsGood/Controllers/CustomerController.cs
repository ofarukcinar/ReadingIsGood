using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.Controllers;

public class CustomerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}