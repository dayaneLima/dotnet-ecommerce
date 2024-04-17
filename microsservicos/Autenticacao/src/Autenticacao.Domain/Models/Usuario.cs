using Autenticacao.Domain.Models.Core;
using Autenticacao.Domain.Validations;
using Autenticacao.Domain.Errors;

namespace Autenticacao.Domain.Models;

public class Usuario : EntityTimestamp
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    
    public override EntityError Validar() => new EntityError(new UsuarioValidation().Validate(this));
}