using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using HelpDeskTI.DTO;
using HelpDeskTI.Repositories;
using System.Threading.Tasks;

namespace HelpDeskTI.Services
{
    public class UsuarioService
    {
        public readonly UsuarioRepositories usuarioRepositories;

        public UsuarioService(UsuarioRepositories usuarioRepositories)
        {
            this.usuarioRepositories = usuarioRepositories;
        }

        public void CriarUsuario(Usuario usuario)
        {
            if (!usuario.Email.Contains("@"))
            {
                throw new Exception("Email inválido. O email deve conter '@'.");
            }

            else if (usuario.Senha.Length < 8)
            {
                throw new Exception("A senha deve ter no mínimo 8 caracteres.");
            }

            usuarioRepositories.SalvarUsuario(usuario);

        }

        public Usuario Login(string email, string senha)
        {
            return usuarioRepositories.login(email, senha);
        }
    }
}
