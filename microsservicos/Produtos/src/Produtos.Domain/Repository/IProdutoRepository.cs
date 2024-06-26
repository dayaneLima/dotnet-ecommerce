using Produtos.Domain.Models;
using Produtos.Domain.Repository.Core;

namespace Produtos.Domain.Repository;

public interface IProdutoRepository: IRepository<Produto> 
{
    Task<IEnumerable<Produto>> ListarPorIds(List<int> ids); 
    Task<IEnumerable<Produto>> ListarPorIdsIncluindoExcluidos(List<int> ids); 
}