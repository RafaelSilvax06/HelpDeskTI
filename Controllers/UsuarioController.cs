using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Services;
using HelpDeskTI.Data;
using HelpDeskTI.DTO;

namespace HelpDeskTI.Controllers 
{ 

	[ApiController]
	[Route("api/usuarios")]
	public class UsuarioController : ControllerBase
	{

		private readonly UsuarioService _usuarioService;

		public UsuarioController(UsuarioService services)
		{
			_usuarioService = services;
		}


		[HttpPost]
		[Route("cadastro")]
		public ActionResult CriarUsuario([FromBody] Usuario usuario)
		{
			try
			{
				_usuarioService.CriarUsuario(usuario);
				return Created("", usuario);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpPost]
		[Route("login")]
		public ActionResult Login([FromBody] LoginRequestDTO request)
		{
			try
			{
				var usuario = _usuarioService.Login(request.Email, request.Senha);
				HelpDeskTI.Sessao.SessaoUsuario.UsuarioLogado = usuario;
				return Ok(usuario);
			}
			catch (Exception ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}
	}
}