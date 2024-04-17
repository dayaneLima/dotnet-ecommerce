using Microsoft.EntityFrameworkCore;
using Autenticacao.Data.Context;
using Autenticacao.Domain.Repository.Core;
using Autenticacao.Domain.Models.Core;

namespace Autenticacao.Data.Repository;

public abstract class Repository<T> : IRepository<T> where T : Entity
{
    protected readonly AutenticacaoContext _context;
    public IUnitOfWork UnitOfWork => _context;
    protected DbSet<T> _dbSet;

    protected Repository(AutenticacaoContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }       

    public virtual T Inserir(T entity) => _dbSet.Add(entity).Entity;

    public virtual T Atualizar(T entity) => _dbSet.Update(entity).Entity;

    public virtual void Excluir(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity is not null) _dbSet.Remove(entity);
    }

    public virtual Task<T?> ObterPorId(int id) => _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

    public virtual async Task<IEnumerable<T>> ObterTodos() => await _dbSet.ToListAsync();

    public void DetachLocal(Func<T, bool> predicate)
    {
        var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
        if (local is not null) _context.Entry(local).State = EntityState.Detached;
    }

    public void Dispose() => _context.Dispose();
}