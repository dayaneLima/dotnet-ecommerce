using Microsoft.EntityFrameworkCore;

using Autenticacao.Domain.Models;
using Autenticacao.Domain.Repository;
using Autenticacao.Data.Context;

namespace Autenticacao.Data.Repository;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AutenticacaoContext context) : base(context) { }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return await _dbSet.Where(e =>  e.Email!.Equals(email)).FirstOrDefaultAsync();
    }
}