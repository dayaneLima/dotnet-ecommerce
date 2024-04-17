using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Produtos.Domain.Repository.Core;
using Produtos.Domain.Models;

namespace Produtos.Data.Context;

public class ProdutoContext(DbContextOptions<ProdutoContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoContext).Assembly);
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
