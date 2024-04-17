namespace Pedidos.Application.DTOs;

public record PedidoRetornoDTO(int Id, double ValorTotal, string? Status, DateTime? DataHorarioCadastro);