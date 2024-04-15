using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produtos.Application.Interfaces;
using Produtos.Application.DTOs;

namespace Produtos.Service.Controllers;

[ApiController]
[Route("v1/produtos")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Produto")]
[Authorize]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoRetornoDTO>> Inserir([FromBody] ProdutoDTO produto)
    {
        return await _produtoService.Inserir(produto);
    }

    [HttpPut]
    [Route("{idProduto}")]
    public async Task<ActionResult<ProdutoRetornoDTO>> Atualizar(int idProduto, [FromBody] ProdutoDTO produto)
    {
        return await _produtoService.Atualizar(idProduto, produto);
    }

    [HttpGet]
    [Route("{idProduto}")]
    public async Task<ActionResult<ProdutoRetornoDTO>> Obter(int idProduto)
    {
        return await _produtoService.Obter(idProduto);
    }

    [HttpDelete]
    [Route("{idProduto}")]
    public async Task Excluir(int idProduto)
    {
        await _produtoService.Excluir(idProduto);
    }

    [HttpGet]
    public async Task<IEnumerable<ProdutoRetornoDTO>> Listar()
    {
        return await _produtoService.Listar();
    }
}