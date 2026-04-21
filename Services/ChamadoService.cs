using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using HelpDeskTI.Repositories;
using HelpDeskTI.Sessao;


namespace HelpDeskTI.Services
{

    public class ChamadoService
    {
        private readonly ChamadoRepositories chamadoRespositories;

        public ChamadoService(ChamadoRepositories chamadoRespositories)
        {
            this.chamadoRespositories = chamadoRespositories;
        }

        public void CriarChamado(Chamado chamado)
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }

            chamado.Solicitante = SessaoUsuario.UsuarioLogado;

            chamadoRespositories.SalvarChamado(chamado);
        }

    }
}
