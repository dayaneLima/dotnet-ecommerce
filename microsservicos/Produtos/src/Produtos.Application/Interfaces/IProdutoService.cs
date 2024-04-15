using Produtos.Application.DTOs;

namespace Produtos.Application.Interfaces;

public interface IProdutoService
{
    Task<ProdutoDTO> Inserir(ProdutoDTO produtoDTO);
}