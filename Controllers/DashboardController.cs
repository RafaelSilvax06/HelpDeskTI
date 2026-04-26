using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using HelpDeskTI.Data;

public class DashboardController : Controller
{

    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Analista()
    {
        var chamados = _context.Chamados.ToList();
        return View(chamados);
    }

    public IActionResult Cliente()
    {
        var chamados = _context.Chamados.ToList();
        return View(chamados);
    }

}

