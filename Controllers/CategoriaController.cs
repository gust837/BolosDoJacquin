using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaRepository;

        public CategoriaController(ICategoria categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _categoriaRepository.Listar());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar(CategoriaDTO dto)
        {
            var categoria = new Categoria { NomeCategoria = dto.NomeCategoria };
            await _categoriaRepository.Cadastrar(categoria);
            return Ok("Categoria cadastrada com sucesso.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Atualizar(Guid id, CategoriaDTO dto)
        {
            var categoria = new Categoria { NomeCategoria = dto.NomeCategoria };
            await _categoriaRepository.Atualizar(id, categoria);
            return Ok("Categoria atualizada com sucesso.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _categoriaRepository.Deletar(id);
            return Ok("Categoria deletada com sucesso.");
        }
    }
}
