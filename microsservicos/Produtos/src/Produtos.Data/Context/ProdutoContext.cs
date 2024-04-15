using Microsoft.EntityFrameworkCore;
using Produtos.Domain.Repository.Core;
using Produtos.Domain.Models;

namespace Produtos.Data.Context;

public class ProdutoContext : DbContext, IUnitOfWork
{                
    public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataHorarioCadastro") != null))
        {               
            entry.Property("DataHorarioAtualizacao").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Added)
                entry.Property("DataHorarioCadastro").CurrentValue = DateTime.Now;
                
            if (entry.State == EntityState.Modified)
            {                
                entry.Property("DataHorarioCadastro").IsModified = false;                
                entry.Property("DataHorarioAtualizacao").IsModified = true;                
            }
        }
        
        return await base.SaveChangesAsync() > 0;
    }
}
