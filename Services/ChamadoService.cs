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
            chamado.Status = 0;

            chamadoRespositories.SalvarChamado(chamado);
        }

        public List<Chamado> ListarChamadosAbertos()
        {
            return chamadoRespositories.chamadoAberto();
        }

        public List<Chamado> ListarChamadosAnalista()
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }

            return chamadoRespositories.chamadoAnalista(SessaoUsuario.UsuarioLogado);
        }

        public List<Chamado> ListaChamadoUsuario()
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }
            return chamadoRespositories.chamadoUsuario(SessaoUsuario.UsuarioLogado);
        }

        public List<Chamado> TodosChamados()
        {

            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }
            return chamadoRespositories.todosChamados(SessaoUsuario.UsuarioLogado);
        }

        public void AtualizarChamado(Chamado chamado)
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }
            chamadoRespositories.AtualizarChamado(chamado, SessaoUsuario.UsuarioLogado);
        }

        public Chamado DetalhesChamado(long id)
        {
            if(id <= 0)
            {
                throw new Exception("ID de chamado inválido.");
            }
            return chamadoRespositories.DetalhesChamado(id);
        }

        public Chamado AtenderChamado(long id)
        {
            if (SessaoUsuario.UsuarioLogado == null)
            {
                throw new Exception("Usuário não está logado.");
            }
            if(id <= 0)
            {
                throw new Exception("ID de chamado inválido.");
            }
            return chamadoRespositories.atenderChamado(id, SessaoUsuario.UsuarioLogado);
        }

    }
}
