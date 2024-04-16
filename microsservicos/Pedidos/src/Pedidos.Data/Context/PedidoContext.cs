using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Repository.Core;
using Pedidos.Domain.Models;

namespace Pedidos.Data.Context;

public class PedidoContext : DbContext, IUnitOfWork
{                
    public PedidoContext(DbContextOptions<PedidoContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
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
