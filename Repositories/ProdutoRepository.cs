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
    public class ProdutoRepository : IProduto
    {
        private readonly BolosContext _context;

        public ProdutoRepository(BolosContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Produto produto)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);
            if (produtoBuscado != null)
            {
                produtoBuscado.NomeProduto = produto.NomeProduto;
                produtoBuscado.Preco = produto.Preco;
                produtoBuscado.ImagemUrl = produto.ImagemUrl;
                produtoBuscado.DescricaoCurta = produto.DescricaoCurta;
                produtoBuscado.DescricaoLonga = produto.DescricaoLonga;
                produtoBuscado.Disponibilidade = produto.Disponibilidade;
                produtoBuscado.Situacao = produto.Situacao;
                produtoBuscado.IdCategoria = produto.IdCategoria;

                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AtualizarDisponibilidade(Guid id, bool disponibilidade)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);
            if (produtoBuscado != null)
            {
                produtoBuscado.Disponibilidade = disponibilidade;
                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AtualizarSituacao(Guid id, string situacao)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);
            if (produtoBuscado != null)
            {
                produtoBuscado.Situacao = situacao;
                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Produto?> BuscarPorId(Guid id)
        {
            return await _context.Produto.FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task Cadastrar(Produto produto)
        {
            await _context.Produto.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var obj = await BuscarPorId(id);
            if (obj != null) {
                var temAvaliacao = await _context.Avaliacao.AnyAsync(a => a.IdProduto == id);
                if (temAvaliacao) throw new BolosDoJacquin.Exceptions.ConflictException("Não é possível excluir este produto pois ele possui avaliações.");
                _context.Produto.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Produto>> Filtrar(Guid? idCategoria, decimal? precoMin, decimal? precoMax, string termoBusca)
        {
            var query = _context.Produto.AsQueryable();

            if (idCategoria.HasValue)
                query = query.Where(p => p.IdCategoria == idCategoria);

            if (precoMin.HasValue)
                query = query.Where(p => p.Preco >= precoMin.Value);

            if (precoMax.HasValue)
                query = query.Where(p => p.Preco <= precoMax.Value);

            if (!string.IsNullOrEmpty(termoBusca))
                query = query.Where(p => p.NomeProduto.Contains(termoBusca) || p.DescricaoCurta.Contains(termoBusca));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<Produto>> Listar()
        {
            return await _context.Produto.AsNoTracking().ToListAsync();
        }
    }
}
