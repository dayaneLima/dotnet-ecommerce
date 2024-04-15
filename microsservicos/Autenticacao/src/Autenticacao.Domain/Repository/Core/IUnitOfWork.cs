namespace Autenticacao.Domain.Repository.Core;

public interface IUnitOfWork
{
    Task<bool> Commit();
}