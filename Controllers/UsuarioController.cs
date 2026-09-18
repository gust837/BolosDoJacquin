using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] UsuarioDTO dto)
        {
            
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    Perfil = "Cliente",
                    Situacao = "Ativo"
                };

                await _usuarioRepository.Cadastrar(usuario);
                return Ok("Usuário cadastrado com sucesso.");
            
        }

        [HttpPost("admin")]
        public async Task<IActionResult> CadastrarAdmin([FromBody] UsuarioDTO dto)
        {
            
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    Perfil = "Administrador",
                    Situacao = "Ativo"
                };

                await _usuarioRepository.Cadastrar(usuario);
                return Ok("Administrador cadastrado com sucesso.");
            
        }

        [HttpDelete("{idUsuario}/desativar-conta")]
        public async Task<IActionResult> DesativarConta(Guid idUsuario)
        {
            
                await _usuarioRepository.AtualizarSituacao(idUsuario, "Inativo");
                return Ok("Usuário desativado com sucesso.");
            
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarUsuarios()
        {
            
                return Ok(await _usuarioRepository.Listar());
            
        }
    }
}

