using Microsoft.AspNetCore.Mvc;

namespace HelpDeskTI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}