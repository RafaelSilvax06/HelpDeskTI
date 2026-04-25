using Microsoft.AspNetCore.Mvc;

namespace HelpDeskTI.Controllers
{
    public class ChamadosController : Controller
    {
        public IActionResult Novo()
        {
            return View();
        }
    }
}