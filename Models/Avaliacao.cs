using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models;

[Index("IdUsuario", "IdProduto", Name = "UQ_Avaliacao_Usuario_Bolo", IsUnique = true)]
public partial class Avaliacao
{
    [Key]
    [Column("idAvaliacao")]
    public Guid IdAvaliacao { get; set; }

    public byte Nota { get; set; }

    [Unicode(false)]
    public string? Comentario { get; set; }

    public bool Situacao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    [Unicode(false)]
    public string? MotivoOcultacao { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdProduto { get; set; }

    [ForeignKey("IdProduto")]
    [InverseProperty("Avaliacao")]
    public virtual Produto? IdProdutoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Avaliacao")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}

