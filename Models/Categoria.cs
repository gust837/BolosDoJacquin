using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models;

[Index("NomeCategoria", Name = "UQ__Categori__98459A0B9DD732EE", IsUnique = true)]
public partial class Categoria
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string NomeCategoria { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
