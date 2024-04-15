using Microsoft.EntityFrameworkCore;
using Produtos.Data.Context;
using Produtos.Domain.Repository.Core;

namespace Produtos.Data.Repository;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly ProdutoContext _context;
    public IUnitOfWork UnitOfWork => _context;
    protected DbSet<T> _dbSet;

    protected Repository(ProdutoContext context)
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

    public virtual IEnumerable<T> ObterTodos() => _dbSet.ToList();

    public void DetachLocal(Func<T, bool> predicate)
    {
        var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
        if (local != null) _context.Entry(local).State = EntityState.Detached;
    }

    public void Dispose() => _context.Dispose();
}