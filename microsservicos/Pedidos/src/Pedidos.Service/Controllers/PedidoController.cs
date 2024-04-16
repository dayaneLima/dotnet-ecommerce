using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Interfaces;
using Pedidos.Application.DTOs;
using System.Security.Claims;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Service.Controllers;

[ApiController]
[Route("v1/pedidos")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Pedido")]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PedidoController(IPedidoService pedidoService, IHttpContextAccessor httpContextAccessor)
    {
        _pedidoService = pedidoService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public void Inserir([FromBody] PedidoDTO pedido)
    {
        string idUsuarioToken = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        if (!int.TryParse(idUsuarioToken, out int idUsuario))
            throw new NotFoundException("Usuário não identidicado");

        _pedidoService.InserirNaFila(idUsuario, pedido);
    }

    // [HttpPut]
    // [Route("{idPedido}")]
    // public async Task<ActionResult<PedidoRetornoDTO>> Atualizar(int idPedido, [FromBody] PedidoDTO pedido)
    // {
    //     return await _pedidoService.Atualizar(idPedido, pedido);
    // }

    // [HttpGet]
    // [Route("{idPedido}")]
    // public async Task<ActionResult<PedidoRetornoDTO>> Obter(int idPedido)
    // {
    //     return await _pedidoService.Obter(idPedido);
    // }

    // [HttpDelete]
    // [Route("{idPedido}")]
    // public async Task Excluir(int idPedido)
    // {
    //     await _pedidoService.Excluir(idPedido);
    // }

    // [HttpGet]
    // public async Task<IEnumerable<PedidoRetornoDTO>> Listar()
    // {
    //     return await _pedidoService.Listar();
    // }
}