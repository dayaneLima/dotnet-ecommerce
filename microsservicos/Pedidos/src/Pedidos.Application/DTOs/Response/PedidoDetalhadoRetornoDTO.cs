namespace Pedidos.Application.DTOs;

public record PedidoDetalhadoRetornoDTO(int Id, double ValorTotal, string? Status, DateTime? DataHorarioCadastro, IEnumerable<ItemPedidoRetornoDTO> ItensPedido);