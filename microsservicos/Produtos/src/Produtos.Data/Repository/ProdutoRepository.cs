using Microsoft.EntityFrameworkCore;

using Produtos.Domain.Models;
using Produtos.Domain.Repository;
using Produtos.Data.Context;

namespace Produtos.Data.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ProdutoContext context) : base(context) { }

    public async Task<IEnumerable<Produto>> ListarPorIds(List<int> ids)
    {
        return await _dbSet.Where(e =>  ids.Contains(e.Id)).ToListAsync<Produto>();
    }

    public async Task<IEnumerable<Produto>> ListarPorIdsIncluindoExcluidos(List<int> ids)
    {
        return await _dbSet.IgnoreQueryFilters().Where(e =>  ids.Contains(e.Id)).ToListAsync<Produto>();
    }
}