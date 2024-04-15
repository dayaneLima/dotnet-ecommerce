using Produtos.Domain.Models;
using Produtos.Domain.Repository;
using Produtos.Data.Context;

namespace Produtos.Data.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ProdutoContext context) : base(context) { }
}