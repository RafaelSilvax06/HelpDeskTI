using Microsoft.AspNetCore.Mvc;
using HelpDeskTI.Models;
using HelpDeskTI.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

            if (chamado.Categoria == null)
            {
                throw new Exception("Categoria não pode ser null.");

            }
            else if (chamado.Titulo == null)
            {
                throw new Exception("Já existe um chamado com este título.");
            }
            else if (chamado.Descricao == null)
            {
                throw new Exception("Já existe um chamado com esta descrição.");
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
                .Where(c => c.Status == StatusChamado.Aberto)
                .ToList();
        }

        public List<Chamado> chamadoAnalista(Usuario analista)
        {
            var chamadosDoAnalista = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .Where(c => c.Analista != null && c.Analista.Id == analista.Id && c.Status == StatusChamado.EmAndamento)
                .ToList();

            if (chamadosDoAnalista.Count == 0)
            {
                throw new Exception("Não existe nenhum chamado em andamento com este analista atribuído.");
            }

            return chamadosDoAnalista;
        }

        public List<Chamado> chamadoUsuario(Usuario usuario)
        {
            var chamadoUsuario = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Analista)
                .Where(c => c.Solicitante != null && c.Solicitante.Id == usuario.Id).ToList();

            if (chamadoUsuario.Count == 0)
            {
                throw new Exception("Não existe nenhum chamado para este usuário.");
            }

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

        public void AtualizarChamado(Chamado chamado, Usuario usuario)
        {
            // Busca o chamado no banco já incluindo o relacionamento de analista
            var chamadoExistente = _context.Chamados
                .Include(c => c.Analista)
                .FirstOrDefault(c => c.Id == chamado.Id);

            if (chamadoExistente == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            // Permite ao analista assumir (adicionar) o chamado a ele
            if (chamado.Analista != null && chamadoExistente.Analista == null)
            {
                atenderChamado(chamadoExistente.Id, chamado.Analista);
            }

            if (chamadoExistente.Analista == null)
            {
                throw new Exception("Os dados do chamado só podem ser atualizados após ser atribuído a um analista.");
            }
            else if (chamado.Analista == null || chamadoExistente.Analista.Id != chamado.Analista.Id)
            {
                throw new Exception("Você só pode atualizar os dados de um chamado que está atribuído a você.");
            }

            // Atualiza os campos liberados
            chamadoExistente.Status = chamado.Status;
            chamadoExistente.Prioridade = chamado.Prioridade;
            chamadoExistente.Comentarios = chamado.Comentarios;

            // Atualiza a data de alteração
            chamadoExistente.DataAtualizacao = DateTime.Now;

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
            if (chamadoExistente.Analista != null)
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

       
    }
}