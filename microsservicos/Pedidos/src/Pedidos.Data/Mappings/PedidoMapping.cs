using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Models;

namespace Pedidos.Data.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {              
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnType("int(10) unsigned");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ValorTotal)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.IdUsuario)
            .IsRequired()
            .HasColumnType("int(10) unsigned");

        builder.Property(e => e.DataHorarioCadastro).IsRequired();
        builder.Property(e => e.DataHorarioAtualizacao).IsRequired();
        builder.Property(e => e.DataHorarioExclusao).IsRequired(false);

        builder.HasMany(e => e.ItensPedido)
            .WithOne(e => e.Pedido)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Pedidos");
        
        builder.HasQueryFilter(e => e.DataHorarioExclusao == null);
    }
}   