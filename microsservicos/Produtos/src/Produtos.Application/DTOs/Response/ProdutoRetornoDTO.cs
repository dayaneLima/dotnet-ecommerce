namespace Produtos.Application.DTOs;

public record ProdutoRetornoDTO(int Id, string? Nome, string? Descricao, double Valor, string? Categoria, int QuantidadeDisponivel, string? UrlImagem);