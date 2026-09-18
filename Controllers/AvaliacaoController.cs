using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacaoRepository;

        public AvaliacaoController(IAvaliacao avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        [HttpPatch("{id}/ocultar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AlterarSituacao(Guid id, [FromBody] bool situacao, [FromQuery] string motivoOcultacao)
        {
            await _avaliacaoRepository.AlterarSituacao(id, situacao, motivoOcultacao);
            return Ok("Situação da avaliação alterada com sucesso.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Atualizar(Guid id, AvaliacaoDTO dto)
        {
            var avaliacao = new Avaliacao
            {
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                Situacao = true
            };
            await _avaliacaoRepository.Atualizar(id, avaliacao);
            return Ok("Avaliação atualizada com sucesso.");
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Cadastrar(AvaliacaoDTO dto)
        {
            var idUsuario = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var jaAvaliou = await _avaliacaoRepository.BuscarPorUsuarioEProduto(idUsuario, dto.IdProduto);
            if (jaAvaliou != null) {
                throw new BolosDoJacquin.Exceptions.ConflictException("Você já avaliou este produto. Edite a avaliação existente.");
            }

            var avaliacao = new Avaliacao
            {
                IdProduto = dto.IdProduto,
                IdUsuario = idUsuario,
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                Situacao = true,
                DataCriacao = DateTime.Now
            };
            await _avaliacaoRepository.Cadastrar(avaliacao);
            return Ok("Avaliação postada com sucesso.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _avaliacaoRepository.Deletar(id);
            return Ok("Avaliação deletada com sucesso.");
        }

        [HttpGet("produto/{idProduto}")]
        public async Task<IActionResult> ListarPorProduto(Guid idProduto)
        {
            return Ok(await _avaliacaoRepository.ListarPorProduto(idProduto));
        }

        [HttpGet("usuario/{idUsuario}")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> ListarPorUsuario(Guid idUsuario)
        {
            return Ok(await _avaliacaoRepository.ListarPorUsuario(idUsuario));
        }

        [HttpGet("admin/todas")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarTodasAdmin()
        {
            return Ok(await _avaliacaoRepository.ListarTodasAdmin());
        }
    }
}
