using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Autenticacao.Domain.Models;

namespace Autenticacao.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {              
        builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("int(10) unsigned");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

        builder.Property(e => e.DataHorarioCadastro).IsRequired();

        builder.ToTable("Usuarios");
    }
}   