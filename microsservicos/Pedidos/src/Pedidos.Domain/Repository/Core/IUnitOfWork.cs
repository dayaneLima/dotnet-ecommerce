namespace Pedidos.Domain.Repository.Core;

public interface IUnitOfWork
{
    Task<bool> Commit();
}