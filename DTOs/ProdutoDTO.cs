using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class ProdutoDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string NomeProduto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Preco { get; set; }

        public string? ImagemUrl { get; set; }

        public string? DescricaoCurta { get; set; }

        public string? DescricaoLonga { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public Guid IdCategoria { get; set; }
    }
}
