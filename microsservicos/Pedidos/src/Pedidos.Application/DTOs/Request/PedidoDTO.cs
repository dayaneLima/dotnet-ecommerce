using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record PedidoDTO
{
    [Required(ErrorMessage = "Itens do pedido é obrigatório"), MinLength(1, ErrorMessage = "Informe pelo menos {1} item para o pedido")]
    public IEnumerable<ItemPedidoDTO>? ItensPedido {get; init;}
}