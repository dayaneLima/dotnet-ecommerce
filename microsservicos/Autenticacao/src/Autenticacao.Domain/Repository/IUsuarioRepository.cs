using Autenticacao.Domain.Models;
using Autenticacao.Domain.Repository.Core;

namespace Autenticacao.Domain.Repository;

public interface IUsuarioRepository: IRepository<Usuario>
{
    Task<Usuario?> ObterPorEmail(string email);
}