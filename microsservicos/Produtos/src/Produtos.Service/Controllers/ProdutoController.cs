using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Produtos.Application.Interfaces;
using Produtos.Application.DTOs;

namespace Produtos.Service.Controllers;

[ApiController]
[Route("v1/produtos")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Produto")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProdutoDTO>> Inserir([FromBody] ProdutoDTO produto)
    {
        return await _produtoService.Inserir(produto);
    }

    // [HttpGet]
    // [Authorize]
    // [Route("authenticated")]
    // public string Authenticated() => String.Format("Authenticated - {0}", "asdas");
    // public string Authenticated() => String.Format("Authenticated - {0}", User?.Identity?.Name);
}