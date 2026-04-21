using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Analista()
    {
        return View();
    }

    public IActionResult Cliente()
    {
        return View();
    }
}