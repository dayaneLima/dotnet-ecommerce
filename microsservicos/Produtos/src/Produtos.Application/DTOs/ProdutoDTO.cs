using System.ComponentModel.DataAnnotations;

namespace Produtos.Application.DTOs;

public record ProdutoDTO
{
    [Required(ErrorMessage = "Nome é obrigatório", AllowEmptyStrings = false)]
    public required string Nome {get; init;}

    [Required(ErrorMessage = "Descrição é obrigatória", AllowEmptyStrings = false)]    
    public required string Descricao {get; init;}

    [Required(ErrorMessage = "Valor é obrigatório", AllowEmptyStrings = false)]
    public double Valor { get; set; }
    
    [Required(ErrorMessage = "Categoria é obrigatória", AllowEmptyStrings = false)]    
    public required string Categoria { get; set; }
    
    [Required(ErrorMessage = "QuantidadeDisponivel é obrigatória", AllowEmptyStrings = false)]    
    public int QuantidadeDisponivel { get; set; }
    
    [Required(ErrorMessage = "UrlImagem é obrigatória", AllowEmptyStrings = false)]    
    public required string UrlImagem { get; set; }
}