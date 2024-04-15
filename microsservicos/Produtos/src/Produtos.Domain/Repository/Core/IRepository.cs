using Produtos.Domain.Models.Core;

namespace Produtos.Domain.Repository.Core;

public interface IRepository<T> : IDisposable where T : Entity
{
    IUnitOfWork UnitOfWork { get; }

    T Inserir(T entity);
    T Atualizar(T entity);
    void Excluir(int id);
    Task<T> ObterPorId(int id);
    Task<IEnumerable<T>> ObterTodos();
    void DetachLocal(Func<T, bool> predicate);
}