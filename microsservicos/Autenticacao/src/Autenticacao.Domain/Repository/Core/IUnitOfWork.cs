using System.Threading.Tasks;

namespace Autenticacao.Domain.Repository.Core;

public interface IUnitOfWork
{
    Task<bool> Commit();
}