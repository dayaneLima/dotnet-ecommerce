using Produtos.Application.Interfaces;
using Produtos.Domain.Repository;
using Produtos.Domain.Exceptions;
using Produtos.Application.DTOs;

namespace Produtos.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<ProdutoDTO> Inserir(ProdutoDTO produtoDTO)
    {
        return produtoDTO;
    }
}
  