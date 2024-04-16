using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Models;

namespace Pedidos.Data.Mappings;

public class ItemPedidoMapping : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {                     
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnType("int(10) unsigned");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Quantidade)
            .IsRequired()
            .HasColumnType("int(10) unsigned");

        builder.Property(e => e.PrecoVenda)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(e => e.IdProduto)
            .IsRequired()
            .HasColumnType("int(10) unsigned");

        builder.Property(e => e.DataHorarioCadastro).IsRequired();
        builder.Property(e => e.DataHorarioAtualizacao).IsRequired();
        builder.Property(e => e.DataHorarioExclusao).IsRequired(false);

        builder.ToTable("ItensPedido");
        
        builder.HasQueryFilter(e => e.DataHorarioExclusao == null);
    }
}   