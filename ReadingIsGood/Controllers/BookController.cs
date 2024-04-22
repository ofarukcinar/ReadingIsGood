using Microsoft.AspNetCore.Mvc;

namespace ReadingIsGood.Controllers;

public class BookController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}