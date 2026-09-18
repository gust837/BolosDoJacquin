using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models;

public partial class Produto
{
    [Key]
    public Guid IdProduto { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NomeProduto { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Preco { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ImagemUrl { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DescricaoCurta { get; set; }

    public string? DescricaoLonga { get; set; }

    public bool Disponibilidade { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Situacao { get; set; } = null!;

    public Guid? IdCategoria { get; set; }

    [InverseProperty("IdProdutoNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Produto")]
    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
