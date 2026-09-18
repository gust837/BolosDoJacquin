using BolosDoJacquin.BdContextBolos;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using BolosDoJacquin.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly BolosContext _context;

        public UsuarioRepository(BolosContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Usuario usuario)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Nome = usuario.Nome;
                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;
                usuarioBuscado.Situacao = usuario.Situacao;
                usuarioBuscado.Perfil = usuario.Perfil;
                _context.Usuario.Update(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AtualizarSituacao(Guid id, string situacao)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);
            if (usuarioBuscado != null)
            {
                usuarioBuscado.Situacao = situacao;
                _context.Usuario.Update(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> BuscarPorEmailESenha(string email, string senha)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
                return null;

            bool senhaValida = Criptografia.CompararHash(senha, usuario.Senha);

            if (!senhaValida)
                return null;

            return usuario;
        }

        public async Task<Usuario?> BuscarPorId(Guid id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(t => t.IdUsuario == id);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            usuario.Senha = Criptografia.GerarHash(usuario.Senha);
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuario.AsNoTracking().ToListAsync();
        }
    }
}
