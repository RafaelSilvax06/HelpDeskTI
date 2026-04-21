using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Services;
using HelpDeskTI.Data;
using HelpDeskTI.DTO;
using HelpDeskTI.Sessao;


namespace HelpDeskTI.Controllers
{

    [ApiController]
    [Route("api/chamado")]
    public class ChamadoController : ControllerBase
    {

        private readonly ChamadoService _chamadoService;

        public ChamadoController(ChamadoService services)
        {
            _chamadoService = services;
        }


        [HttpPost]
        [Route("novoChamado")]
        public ActionResult NovoChamado([FromBody] Chamado chamado)
        {
            _chamadoService.CriarChamado(chamado);
            return Created("", chamado);
        }
    }
}