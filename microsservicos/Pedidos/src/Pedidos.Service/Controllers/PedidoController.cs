using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Interfaces;
using Pedidos.Application.DTOs;

namespace Pedidos.Service.Controllers;

[ApiController]
[Route("v1/pedidos")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Pedido")]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    // [HttpPost]
    // public async Task Inserir([FromBody] PedidoDTO pedido)
    // {
    //     // return await _pedidoService.Inserir(pedido);
    // }

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