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



        // Endpoint dos clientes

        [HttpPost]
        [Route("novoChamado")]
        public ActionResult NovoChamado([FromBody] Chamado chamado)
        {
            _chamadoService.CriarChamado(chamado);
            return Created("", chamado);
        }

        [HttpPost]
        [Route("listarChamados")]
        public List<Chamado> ListarChamados()
        {
            var chamados = _chamadoService.ListaChamadoUsuario();
            return chamados;
        }

        // Endpoint dos analistas

        [HttpPost]
        [Route("ChamadosAberto")]
        public List<Chamado> ListarChamadosAberto()
        {
            var chamadosAbertos = _chamadoService.ListarChamadosAbertos();
            return chamadosAbertos;
        }

        [HttpPost]
        [Route("ChamadosAnalista")]
        public List<Chamado> ListarChamadosAnalista()
        {
            var chamadosAnalista = _chamadoService.ListarChamadosAnalista();
            return chamadosAnalista;

        }


        [HttpPost]
        [Route("atualizarChamado")]
        public ActionResult AtualizarChamado([FromBody] Chamado chamado)
        {
            _chamadoService.AtualizarChamado(chamado);
            return Ok(chamado);
        }


        [HttpPost]
        [Route("atenderChamado/{id}")]
        public ActionResult AtenderChamado([FromRoute] long id)
        {
            _chamadoService.AtenderChamado(id);
            return Ok();
        }

        [HttpPost]
        [Route("detalhesChamado/{id}")]
        public ActionResult DetalhesChamado([FromRoute] long id)
        {
            var chamado = _chamadoService.DetalhesChamado(id);
            return Ok(chamado);
        }
    }
}