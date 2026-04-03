using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;

namespace HelpDeskTI.Controllers 
{ 

	[ApiController]
	[Route("api/[Controller]")]
	public class UsuarioController : ControllerBase
    {

		private readonly AppDbContext _context;

		public UsuarioController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult CriarUsuario([FromBody] Usuario usuario)
		{
			_context.Usuarios.Add(usuario);
			_context.SaveChanges();

			return Ok(usuario);
		}
	}
}