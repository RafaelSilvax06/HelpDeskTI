using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using System.Linq;
using System;

namespace HelpDeskTI.Repositories
{
    public class UsuarioRepositories
    {

        private readonly AppDbContext _context;

        public UsuarioRepositories(AppDbContext context)
        {
            _context = context;
        }

        public void SalvarUsuario(Usuario usuario)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Email == usuario.Email);
            if (usuarioExistente != null)
            {
                throw new Exception("Já existe um usuário com este email.");
            }
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario login(string email, string senha)
        {

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);
            
            if (usuario == null)
            {
                throw new Exception("Email ou senha inválidos.");
            }
            return usuario;
        }
    }
}
