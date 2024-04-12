using Autenticacao.Domain.Models;
using Autenticacao.Domain.Repository.Core;
using System.Collections.Generic;

namespace Autenticacao.Domain.Repository;

public interface IUsuarioRepository: IRepository<Usuario>
{
    Task<Usuario> ObterPorEmailSenha(string email, string senha);
}