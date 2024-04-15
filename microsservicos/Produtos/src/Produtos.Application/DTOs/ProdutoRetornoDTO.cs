using System.ComponentModel.DataAnnotations;

namespace Produtos.Application.DTOs;

public record ProdutoRetornoDTO
{
    public required int Id {get; init;}
    public required string Nome {get; init;}

    public required string Descricao {get; init;}

    public decimal Valor { get; set; }
    
    public required string Categoria { get; set; }
    
    public int QuantidadeDisponivel { get; set; }

    public required string UrlImagem { get; set; }
}