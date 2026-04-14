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
			_usuarioService.CriarUsuario(usuario);
			return Created("",usuario);
		}

		[HttpPost]
		[Route("login")]
		public ActionResult Login([FromBody] LoginRequestDTO request)
		{
            var usuario = _usuarioService.Login(request.Email, request.Senha);
            return Ok(usuario);
        }
    }
}