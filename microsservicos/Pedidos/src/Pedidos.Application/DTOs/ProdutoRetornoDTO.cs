using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.DTOs;

public record ProdutoRetornoDTO
{
    public required int Id {get; init;}
    public required string Nome {get; init;}

    public required string Descricao {get; init;}

    public double Valor { get; set; }
    
    public required string Categoria { get; set; }
    
    public int QuantidadeDisponivel { get; set; }

    public required string UrlImagem { get; set; }
}