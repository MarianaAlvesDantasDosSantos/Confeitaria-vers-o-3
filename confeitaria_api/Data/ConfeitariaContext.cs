using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using confeitaria_api.Models;

namespace confeitaria_api.Data;

public partial class ConfeitariaContext : DbContext
{
    public ConfeitariaContext(DbContextOptions<ConfeitariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ItemPedido> ItensPedido { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.CategoriaNome, "categoria_nome").IsUnique();

            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_id");
            entity.Property(e => e.CategoriaDescricao)
                .HasMaxLength(150)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("categoria_descricao");
            entity.Property(e => e.CategoriaNome)
                .HasMaxLength(50)
                .HasColumnName("categoria_nome");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.ClienteCpf, "cliente_cpf").IsUnique();

            entity.HasIndex(e => e.ClienteEmail, "cliente_email").IsUnique();

            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.ClienteCpf)
                .HasMaxLength(14)
                .HasColumnName("cliente_cpf");
            entity.Property(e => e.ClienteDataCadastro)
                .HasColumnType("date")
                .HasColumnName("cliente_data_cadastro");
            entity.Property(e => e.ClienteEmail)
                .HasMaxLength(100)
                .HasColumnName("cliente_email");
            entity.Property(e => e.ClienteNome)
                .HasMaxLength(100)
                .HasColumnName("cliente_nome");
            entity.Property(e => e.ClienteTelefone)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("cliente_telefone");
        });

        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.HasKey(e => new { e.PedidoId, e.ProdutoId }).HasName("PRIMARY");

            entity.ToTable("itens_pedido");

            entity.HasIndex(e => e.ProdutoId, "produto_id");

            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("pedido_id");
            entity.Property(e => e.ProdutoId)
                .HasColumnType("int(11)")
                .HasColumnName("produto_id");
            entity.Property(e => e.ItemPedidoPrecoUnitario)
                .HasPrecision(10)
                .HasColumnName("item_pedido_preco_unitario");
            entity.Property(e => e.ItemPedidoQuantidade)
                .HasColumnType("int(11)")
                .HasColumnName("item_pedido_quantidade");

            entity.HasOne(d => d.Pedido).WithMany(p => p.ItensPedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("itens_pedido_ibfk_1");

            entity.HasOne(d => d.Produto).WithMany(p => p.ItensPedido)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("itens_pedido_ibfk_2");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PRIMARY");

            entity.ToTable("pedidos");

            entity.HasIndex(e => e.ClienteId, "cliente_id");

            entity.Property(e => e.PedidoId)
                .HasColumnType("int(11)")
                .HasColumnName("pedido_id");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.PedidoData)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime")
                .HasColumnName("pedido_data");
            entity.Property(e => e.PedidoStatus)
                .HasMaxLength(30)
                .HasDefaultValueSql("'''Pendente'''")
                .HasColumnName("pedido_status");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pedidos_ibfk_1");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId).HasName("PRIMARY");

            entity.ToTable("produtos");

            entity.HasIndex(e => e.CategoriaId, "categoria_id");

            entity.Property(e => e.ProdutoId)
                .HasColumnType("int(11)")
                .HasColumnName("produto_id");
            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_id");
            entity.Property(e => e.ProdutoDescricao)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("produto_descricao");
            entity.Property(e => e.ProdutoEstoque)
                .HasColumnType("int(11)")
                .HasColumnName("produto_estoque");
            entity.Property(e => e.ProdutoNome)
                .HasMaxLength(100)
                .HasColumnName("produto_nome");
            entity.Property(e => e.ProdutoPreco)
                .HasPrecision(10)
                .HasColumnName("produto_preco");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("produtos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
