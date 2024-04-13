using System;
using System.Linq;
using System.Threading.Tasks;
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
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataHorarioCadastro") != null))
        {
            if (entry.State == EntityState.Added)                
                entry.Property("DataHorarioCadastro").CurrentValue = DateTime.Now;                

            if (entry.State == EntityState.Modified)                
                entry.Property("DataHorarioCadastro").IsModified = false;                
        }
        
        return await base.SaveChangesAsync() > 0;
    }
}
