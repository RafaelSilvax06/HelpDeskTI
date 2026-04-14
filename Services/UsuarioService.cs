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

            else if (!ValidarCPF(usuario.CPF))
            {
                throw new Exception("CPF invalido");
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

        private bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int soma = 0;

            // 1º dígito
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);

            int dv1 = (soma * 10) % 11;
            if (dv1 == 10) dv1 = 0;

            // 2º dígito
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);

            int dv2 = (soma * 10) % 11;
            if (dv2 == 10) dv2 = 0;

            return dv1 == (cpf[9] - '0') &&
                   dv2 == (cpf[10] - '0');
        }
    }
}
