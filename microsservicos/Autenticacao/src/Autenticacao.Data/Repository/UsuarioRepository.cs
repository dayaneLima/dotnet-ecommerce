using Autenticacao.Domain.Models;
using Autenticacao.Domain.Repository;
using Autenticacao.Data.Context;

namespace Autenticacao.Data.Repository;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AutenticacaoContext context) : base(context) { }

    public async Task<Usuario> ObterPorEmailSenha(string email, string senha)
    {
        return await Task.Run(() =>
        {
            return _dbSet.Where(e => e.Email.Equals(email) && e.Senha.Equals(senha)).FirstOrDefault();
        });
    }
}