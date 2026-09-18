using BolosDoJacquin.BdContextBolos;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        private readonly BolosContext _context;

        public CategoriaRepository(BolosContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Categoria categoria)
        {
            var categoriaBuscada = await _context.Categoria.FindAsync(id);
            if (categoriaBuscada != null)
            {
                categoriaBuscada.NomeCategoria = categoria.NomeCategoria;
                _context.Categoria.Update(categoriaBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Categoria?> BuscarPorId(Guid id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var obj = await BuscarPorId(id);
            if (obj != null) {
                var temProduto = await _context.Produto.AnyAsync(p => p.IdCategoria == id);
                if (temProduto) throw new BolosDoJacquin.Exceptions.ConflictException("Não é possível excluir esta categoria pois ela possui produtos.");
                _context.Categoria.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categoria.AsNoTracking().ToListAsync();
        }
    }
}
