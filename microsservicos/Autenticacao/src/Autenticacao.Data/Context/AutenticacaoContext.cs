using Microsoft.EntityFrameworkCore;
using Autenticacao.Domain.Repository.Core;
using Autenticacao.Domain.Models;

namespace Autenticacao.Data.Context;

public class AutenticacaoContext : DbContext, IUnitOfWork
{                
    public AutenticacaoContext(DbContextOptions<AutenticacaoContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutenticacaoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        // foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataHorarioCadastro") != null))
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity.GetType().GetProperty("DataHorarioCadastro") != null) 
            {
                if (entry.State == EntityState.Added) 
                    entry.Property("DataHorarioCadastro").CurrentValue = DateTime.Now;                

                if (entry.State == EntityState.Modified)                 
                    entry.Property("DataHorarioCadastro").IsModified = false;     
            }

            if (entry.Entity.GetType().GetProperty("DataHorarioAtualizacao") != null && entry.State == EntityState.Added || entry.State == EntityState.Modified)
                entry.Property("DataHorarioAtualizacao").CurrentValue = DateTime.Now;                

            if (entry.Entity.GetType().GetProperty("DataHorarioExclusao") != null && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Unchanged;
                entry.Property("DataHorarioExclusao").CurrentValue = DateTime.Now;     
            } 
        }
        
        return await base.SaveChangesAsync() > 0;
    }
}
