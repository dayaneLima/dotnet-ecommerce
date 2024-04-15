using System.ComponentModel.DataAnnotations;

namespace Autenticacao.Application.DTOs;

public record LoginDTO
{
    [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
    public string? Email {get; init;}

    [Required(ErrorMessage = "Senha é obrigatória", AllowEmptyStrings = false)]    
    public string? Senha {get; init;}
}