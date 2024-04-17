using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Autenticacao.Domain.Repository.Core;
using Autenticacao.Domain.Models;

namespace Autenticacao.Data.Context;

public class AutenticacaoContext(DbContextOptions<AutenticacaoContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutenticacaoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            UpdateDataHorarioCadastro(entry);
            UpdateDataHorarioAtualizacao(entry);
            UpdateDataHorarioExclusao(entry);
        }
        
        return await base.SaveChangesAsync() > 0;
    }

    private void UpdateDataHorarioCadastro(EntityEntry entry)
    {
        if (entry.Entity.GetType().GetProperty("DataHorarioCadastro") is null) 
            return;

        if (entry.State == EntityState.Added) 
            entry.Property("DataHorarioCadastro").CurrentValue = DateTime.Now;                

        if (entry.State == EntityState.Modified)                 
            entry.Property("DataHorarioCadastro").IsModified = false; 
    }

    private void UpdateDataHorarioAtualizacao(EntityEntry entry)
    {
        if (entry.Entity.GetType().GetProperty("DataHorarioAtualizacao") != null && (entry.State == EntityState.Added || entry.State == EntityState.Modified))
            entry.Property("DataHorarioAtualizacao").CurrentValue = DateTime.Now;        
    }

    private void UpdateDataHorarioExclusao(EntityEntry entry)
    {
        if (entry.Entity.GetType().GetProperty("DataHorarioExclusao") != null && entry.State == EntityState.Deleted)
        {
            entry.State = EntityState.Unchanged;
            entry.Property("DataHorarioExclusao").CurrentValue = DateTime.Now;     
        } 
    }
}
