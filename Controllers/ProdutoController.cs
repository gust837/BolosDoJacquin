using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoRepository;

        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("catalogo")]
        public async Task<IActionResult> ListarCatalogo()
        {
            var produtos = await _produtoRepository.Listar();
            var catalogo = produtos.Where(p => p.Situacao == "Disponivel").ToList();
            return Ok(catalogo);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar(ProdutoDTO dto)
        {
            var produto = new Produto
            {
                NomeProduto = dto.NomeProduto,
                Preco = dto.Preco,
                ImagemUrl = dto.ImagemUrl,
                DescricaoCurta = dto.DescricaoCurta,
                DescricaoLonga = dto.DescricaoLonga,
                IdCategoria = dto.IdCategoria,
                Situacao = "Disponivel",
                Disponibilidade = true
            };
            await _produtoRepository.Cadastrar(produto);
            return Ok("Produto cadastrado com sucesso.");
        }

        [HttpPut("{idProduto}/mudar-preco")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> MudarPreco(Guid idProduto, [FromBody] decimal preco)
        {
            var produto = await _produtoRepository.BuscarPorId(idProduto);
            if (produto == null) return NotFound("Produto não encontrado.");
            produto.Preco = preco;
            await _produtoRepository.Atualizar(idProduto, produto);
            return Ok("Preço atualizado com sucesso.");
        }

        [HttpPut("{idProduto}/mudar-nome")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> MudarNome(Guid idProduto, [FromBody] string nome)
        {
            var produto = await _produtoRepository.BuscarPorId(idProduto);
            if (produto == null) return NotFound("Produto não encontrado.");
            produto.NomeProduto = nome;
            await _produtoRepository.Atualizar(idProduto, produto);
            return Ok("Nome atualizado com sucesso.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeletarProduto(Guid id)
        {
            await _produtoRepository.Deletar(id);
            return Ok("Produto deletado com sucesso.");
        }
    }
}
