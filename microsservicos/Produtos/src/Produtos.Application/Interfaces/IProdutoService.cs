using Produtos.Application.DTOs;

namespace Produtos.Application.Interfaces;

public interface IProdutoService
{
    Task<ProdutoRetornoDTO> Inserir(ProdutoDTO produtoDTO);
    Task<ProdutoRetornoDTO> Atualizar(int id, ProdutoDTO produtoDTO);
    Task<ProdutoRetornoDTO> Obter(int id);
    Task Excluir(int id);
    Task<IEnumerable<ProdutoRetornoDTO>> Listar(); 
    Task<IEnumerable<ProdutoRetornoDTO>> ListarPorIds(List<int> ids, bool incluirExcluidos); 
}