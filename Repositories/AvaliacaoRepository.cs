using BolosDoJacquin.BdContextBolos;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class AvaliacaoRepository : IAvaliacao
    {
        private readonly BolosContext _context;

        public AvaliacaoRepository(BolosContext context)
        {
            _context = context;
        }

        public async Task AlterarSituacao(Guid id, bool situacao, string motivoOcultacao)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao != null)
            {
                avaliacao.Situacao = situacao;
                // Dependendo do model pode no haver 'motivoOcultacao' em DB, vou ignorar se não tiver.
                _context.Avaliacao.Update(avaliacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Atualizar(Guid id, Avaliacao avaliacao)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FindAsync(id);
            if (avaliacaoBuscada != null)
            {
                avaliacaoBuscada.Nota = avaliacao.Nota;
                avaliacaoBuscada.Comentario = avaliacao.Comentario;
                avaliacaoBuscada.Situacao = avaliacao.Situacao;
                _context.Avaliacao.Update(avaliacaoBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Avaliacao?> BuscarPorId(Guid id)
        {
            return await _context.Avaliacao
                .Include(a => a.IdProdutoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(a => a.IdAvaliacao == id);
        }

        public async Task<Avaliacao?> BuscarPorUsuarioEProduto(Guid idUsuario, Guid idProduto) { return await _context.Avaliacao.FirstOrDefaultAsync(a => a.IdUsuario == idUsuario && a.IdProduto == idProduto); }

        public async Task Cadastrar(Avaliacao avaliacao)
        {
            await _context.Avaliacao.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacao.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Avaliacao>> ListarPorProduto(Guid idProduto)
        {
            return await _context.Avaliacao
                .Include(a => a.IdUsuarioNavigation)
                .Where(a => a.IdProduto == idProduto && a.Situacao == true)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Avaliacao>> ListarPorUsuario(Guid idUsuario)
        {
            return await _context.Avaliacao
                .Include(a => a.IdProdutoNavigation)
                .Where(a => a.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Avaliacao>> ListarTodasAdmin()
        {
            return await _context.Avaliacao
                .Include(a => a.IdProdutoNavigation)
                .Include(a => a.IdUsuarioNavigation)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

