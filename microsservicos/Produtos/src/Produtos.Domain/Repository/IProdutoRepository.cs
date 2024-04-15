using Produtos.Domain.Models;
using Produtos.Domain.Repository.Core;

namespace Produtos.Domain.Repository;

public interface IProdutoRepository: IRepository<Produto> { }