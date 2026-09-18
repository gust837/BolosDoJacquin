using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class AvaliacaoDTO
    {
        [Required]
        [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5.")]
        public byte Nota { get; set; }

        [StringLength(500)]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "Informe o produto que está sendo avaliado.")]
        public Guid IdProduto { get; set; }
    }
}
