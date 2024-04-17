namespace Pedidos.Application.DTOs;

public record PedidoDetalhadoRetornoDTO: PedidoRetornoDTO
{
    public PedidoDetalhadoRetornoDTO(int Id, double ValorTotal, string? Status, DateTime? DataHorarioCadastro, IEnumerable<ItemPedidoRetornoDTO> ItensPedido) : 
        base(Id, ValorTotal, Status, DataHorarioCadastro) { }
}