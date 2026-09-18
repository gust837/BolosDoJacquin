using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models;

[Index("Email", Name = "UQ__Usuario__A9D1053467B51FE0", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(64)]
    [Unicode(false)]
    public string Senha { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Perfil { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Situacao { get; set; } = null!;

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();
}
