using System.ComponentModel.DataAnnotations;

namespace Produtos.Application.DTOs;

public record ProdutoDTO
{
    [Required(ErrorMessage = "Nome é obrigatório", AllowEmptyStrings = false)]
    [StringLength(255, ErrorMessage = "O nome não pode passar de {1} caracteres. ")] 
    public string? Nome {get; init;}

    [Required(ErrorMessage = "Descrição é obrigatória", AllowEmptyStrings = false)]    
    [StringLength(255, ErrorMessage = "A descrição não pode passar de {1} caracteres")] 
    public string? Descricao {get; init;}

    [Required(ErrorMessage = "Valor é obrigatório", AllowEmptyStrings = false)]
    [Range(1, double.MaxValue, ErrorMessage = "O valor do produto deve ser maior que zero")]
    public double Valor { get; set; }
    
    [Required(ErrorMessage = "Categoria é obrigatória", AllowEmptyStrings = false)]   
    [StringLength(255, ErrorMessage = "A categoria não pode passar de {1} caracteres")]  
    public string? Categoria { get; set; }
    
    [Required(ErrorMessage = "Quantidade disponivel é obrigatória", AllowEmptyStrings = false)]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo")]
    public int QuantidadeDisponivel { get; set; }
    
    [Required(ErrorMessage = "UrlImagem é obrigatória", AllowEmptyStrings = false)]
    [Url(ErrorMessage = "A URL fornecida não é válida.")]
    [StringLength(255, ErrorMessage = "A url não pode passar de {1} caracteres")] 
    public string? UrlImagem { get; set; }
}