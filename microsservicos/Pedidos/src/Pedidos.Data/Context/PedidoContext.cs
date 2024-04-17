using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Repository.Core;
using Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pedidos.Data.Context;

public class PedidoContext(DbContextOptions<PedidoContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoContext).Assembly);
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
