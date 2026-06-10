using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HelpDeskTI.DTO;

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

            _context.Chamados.Add(chamado);
            _context.SaveChanges();
        }

        public List<Chamado> chamadoAberto()
        {
            return _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .Where(c => c.Status == StatusChamado.Aberto && c.Analista == null)
                .ToList();
        }

        public List<Chamado> chamadoAnalista(Usuario analista)
        {
            var chamadosDoAnalista = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .Where(c => c.Analista != null && c.Analista.Id == analista.Id && c.Status == StatusChamado.EmAndamento)
                .ToList();

            // if (chamadosDoAnalista.Count == 0)
            // {
            //     throw new Exception("Não existe nenhum chamado em andamento com este analista atribuído.");
            // }

            return chamadosDoAnalista;
        }

        public List<Chamado> chamadoUsuario(Usuario usuario)
        {
            var chamadoUsuario = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .Where(c => c.Solicitante != null && c.Solicitante.Id == usuario.Id).ToList();

            // if (chamadoUsuario.Count == 0)
            // {
            //     throw new Exception("Não existe nenhum chamado para este usuário.");
            // }

            return chamadoUsuario;
        }

        public List<Chamado> todosChamados(Usuario adm)
        {
            var todos = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .ToList();

            if (todos.Count == 0)
            {
                throw new Exception("Não existe nenhum chamado registrado.");
            }

            return todos;
        }

        public void AtualizarChamado(AtualizarChamadoRequestDTO chamado, Usuario usuario)
        {
            // Busca o chamado no banco já incluindo o relacionamento de analista
            var chamadoExistente = _context.Chamados
                .Include(c => c.Analista)
                .FirstOrDefault(c => c.Id == chamado.Id);

            if (chamadoExistente == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            if (chamadoExistente.Analista == null)
            {
                throw new Exception("O chamado só pode ser atualizado após ser atribuído a um analista.");
            }
            else if (chamadoExistente.Analista.Id != usuario.Id)
            {
                throw new Exception("Você só pode atualizar chamados atribuídos a você.");
            }

            // Atualiza os campos liberados
            chamadoExistente.Status = chamado.Status;
            chamadoExistente.Prioridade = chamado.Prioridade;

            // Atualiza a data de alteração
            chamadoExistente.DataAtualizacao = DateTime.Now;

            if (chamado.Status == StatusChamado.Aberto)
            {
                chamadoExistente.Analista = null;
            }

            if (chamado.Status == StatusChamado.Fechado)
            {
                chamadoExistente.DataFechamento = DateTime.Now;
            }
            else
            {
                chamadoExistente.DataFechamento = null;
            }

            _context.SaveChanges();
        }

        public Chamado atenderChamado(long id, Usuario analista)
        {
            var chamadoExistente = _context.Chamados
                .Include(c => c.Analista)
                .FirstOrDefault(c => c.Id == id);

            if (chamadoExistente == null)
            {
                throw new Exception("Chamado não encontrado.");
            }
            if (chamadoExistente.Status == StatusChamado.Fechado)
            {
                throw new Exception("Chamado fechado não pode ser assumido.");
            }
            if (chamadoExistente.Analista != null && chamadoExistente.Status != StatusChamado.Aberto)
            {
                throw new Exception("Este chamado já está atribuído a um analista.");
            }

            _context.Usuarios.Attach(analista);
            chamadoExistente.Analista = analista;
            chamadoExistente.Status = StatusChamado.EmAndamento;
            chamadoExistente.DataAtualizacao = DateTime.Now;

            _context.SaveChanges();

            return chamadoExistente;
        }

        public Chamado DetalhesChamado(long id)
        {
            var chamado = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .FirstOrDefault(c => c.Id == id);

            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            return chamado;
        }

        public void ExcluirChamado(long id, Usuario usuario)
        {
            var chamado = _context.Chamados
                .Include(c => c.Solicitante)
                .FirstOrDefault(c => c.Id == id);

            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            if (chamado.Solicitante == null || chamado.Solicitante.Id != usuario.Id)
            {
                throw new Exception("Você só pode excluir chamados criados por você.");
            }

            _context.Chamados.Remove(chamado);
            _context.SaveChanges();
        }

       
    }
}
