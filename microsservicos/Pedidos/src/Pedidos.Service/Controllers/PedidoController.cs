using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pedidos.Application.Interfaces;
using Pedidos.Application.DTOs;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Service.Controllers;

[Authorize]
[ApiController]
[Route("v1/pedidos")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Pedido")]
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
        _pedidoService.PublicarNaFila(ObterIdUsuarioToken(), pedido);
    }

    [HttpGet]
    [Route("{idPedido}")]
    public async Task<ActionResult<PedidoRetornoDTO>> Obter(int idPedido)
    {
        return await _pedidoService.Obter(ObterIdUsuarioToken(), idPedido);
    }

    [HttpGet]
    [Route("{idPedido}/detalhado")]
    public async Task<ActionResult<PedidoDetalhadoRetornoDTO>> ObterDetalhado(int idPedido)
    {
        return await _pedidoService.ObterComItensPedido(ObterIdUsuarioToken(), idPedido);
    }

    [HttpGet]
    public async Task<IEnumerable<PedidoRetornoDTO>> Listar()
    {
        return await _pedidoService.Listar(ObterIdUsuarioToken());
    }

    [HttpGet]
    [Route("detalhado")]
    public async Task<IEnumerable<PedidoDetalhadoRetornoDTO>> ListarDetalhado()
    {
        return await _pedidoService.ListarComItensPedido(ObterIdUsuarioToken());
    }

    private int ObterIdUsuarioToken()
    {
        string idUsuarioToken = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

        if (!int.TryParse(idUsuarioToken, out int idUsuario))
            throw new NotFoundException("Usuário não identificado");
        
        return idUsuario;
    }
}