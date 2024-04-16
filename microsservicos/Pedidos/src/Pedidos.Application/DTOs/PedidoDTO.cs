using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record PedidoDTO
{
    [Required(ErrorMessage = "Itens do pedido é obrigatório")]
    public required IEnumerable<ItemPedidoDTO> ItensPedido {get; init;}
}