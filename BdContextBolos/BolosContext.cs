using System;
using System.Collections.Generic;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.BdContextBolos;

public partial class BolosContext : DbContext
{
    public BolosContext()
    {
    }

    public BolosContext(DbContextOptions<BolosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacao { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PK__Avaliaca__2A0C83126E533210");

            entity.Property(e => e.IdAvaliacao).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Avaliacao).HasConstraintName("FK__Avaliacao__IdPro__5DCAEF64");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Avaliacao).HasConstraintName("FK__Avaliacao__IdUsu__5CD6CB2B");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A1044DA1B69");

            entity.Property(e => e.IdCategoria).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PK__Produto__2E883C23BE055083");

            entity.Property(e => e.IdProduto).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Disponibilidade).HasDefaultValue(true);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produto).HasConstraintName("FK__Produto__IdCateg__571DF1D5");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97A3C4CD4B");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

