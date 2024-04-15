namespace Produtos.Domain.Repository.Core;

public interface IRepository<T> : IDisposable where T : class
{
    IUnitOfWork UnitOfWork { get; }

    T Inserir(T entity);
    T Atualizar(T entity);
    void Excluir(int id);
    IEnumerable<T> ObterTodos();
    void DetachLocal(Func<T, bool> predicate);
}