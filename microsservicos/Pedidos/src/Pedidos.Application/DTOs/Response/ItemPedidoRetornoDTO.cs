namespace Pedidos.Application.DTOs;

public record ItemPedidoRetornoDTO(int Quantidade, string? Nome, string? Descricao, double Valor, string? Categoria, string? UrlImagem);