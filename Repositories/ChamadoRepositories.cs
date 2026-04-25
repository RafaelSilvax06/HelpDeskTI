using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using System.Linq;
using System;

namespace HelpDeskTI.Repositories
{
    public class ChamadoRepositories
    {
        private readonly AppDbContext _context;

        public ChamadoRepositories(AppDbContext context)
        {
            _context = context;
        }

        public void SalvarChamado(Chamado chamado)
        {
            if (chamado.Titulo == null)
            {
                throw new Exception("Título não pode ser nulo.");
            }

            if (chamado.Descricao == null)
            {
                throw new Exception("Descrição não pode ser nula.");
            }

            if (chamado.Solicitante != null)
            {
                _context.Usuarios.Attach(chamado.Solicitante);
            }

            // Garante que comentários não será nulo para não quebrar a restrição do banco de dados
            chamado.Comentarios ??= "";

            _context.Chamados.Add(chamado);
            _context.SaveChanges();
        }
    }
}