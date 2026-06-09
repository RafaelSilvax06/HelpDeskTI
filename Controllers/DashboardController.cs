using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using HelpDeskTI.Data;
using HelpDeskTI.Services;
using HelpDeskTI.Sessao;

public class DashboardController : Controller
{

    private readonly AppDbContext _context;
    private readonly ChamadoService _chamadoService;

    public DashboardController(AppDbContext context, ChamadoService chamadoService)
    {
        _context = context;
        _chamadoService = chamadoService;
    }

    public IActionResult Analista()
    {
        try
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Inicialmente mostra chamados abertos
            var chamados = _chamadoService.ListarChamadosAbertos();
            ViewBag.UsuarioLogado = SessaoUsuario.UsuarioLogado;
            return View(chamados);
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return RedirectToAction("Index", "Login");
        }
    }

    [HttpGet("api/chamados/abertos")]
    public IActionResult ChamadosAbertos()
    {
        try
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                return Unauthorized(new { message = "Usuário não está logado." });
            }

            var chamados = _chamadoService.ListarChamadosAbertos();
            return Ok(chamados);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("api/chamados/meus-atendimentos")]
    public IActionResult MeusAtendimentos()
    {
        try
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                return Unauthorized(new { message = "Usuário não está logado." });
            }

            var chamados = _chamadoService.ListarChamadosAnalista();
            return Ok(chamados);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    public IActionResult Cliente()
    {
        try
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var chamados = _chamadoService.ListaChamadoUsuario();
            ViewBag.UsuarioLogado = SessaoUsuario.UsuarioLogado;
            return View(chamados);
        }
        catch (Exception ex)
        {
            ViewBag.Erro = ex.Message;
            return RedirectToAction("Index", "Login");
        }
    }

    public IActionResult DetalhesAnalista()
    {
        return View();
    }

    public IActionResult DetalhesUsuario()
    {
        return View();
    }

    public IActionResult AtualizarChamado()
    {
        return View();
    }

}

