namespace Pedidos.Application.DTOs;

public record ProdutoDTO(int Id, string? Nome, string? Descricao, double Valor, string? Categoria, int QuantidadeDisponivel, string? UrlImagem);