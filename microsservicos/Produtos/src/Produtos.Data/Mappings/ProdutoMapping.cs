using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Produtos.Domain.Models;

namespace Produtos.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {              
        builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("int(10) unsigned");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.Valor)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

        builder.Property(e => e.Categoria)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.QuantidadeDisponivel)
                .IsRequired()
                .HasColumnType("int(10) unsigned");

        builder.Property(e => e.UrlImagem)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.DataHorarioCadastro).IsRequired();
        builder.Property(e => e.DataHorarioAtualizacao).IsRequired();
        builder.Property(e => e.DataHorarioExclusao).IsRequired(false);

        builder.ToTable("Produtos");
        
        builder.HasQueryFilter(e => e.DataHorarioExclusao == null);
    }
}   