using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Autenticacao.Data.Context;
using Autenticacao.Domain.Repository.Core;

namespace Autenticacao.Data.Repository;

public abstract class Repository<T> : IRepository<T> where T : class
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

    public virtual void Excluir(int id) => _dbSet.Remove(_dbSet.Find(id));

    public virtual IEnumerable<T> ObterTodos() => _dbSet.ToList();

    public void DetachLocal(Func<T, bool> predicate)
    {
        var local = _context.Set<T>().Local.Where(predicate).FirstOrDefault();
        if (local != null) _context.Entry(local).State = EntityState.Detached;
    }

    public void Dispose() => _context.Dispose();
}