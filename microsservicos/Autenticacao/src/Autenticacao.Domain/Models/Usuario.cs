using Autenticacao.Domain.Models.Core;
using Autenticacao.Domain.Validations;
using Autenticacao.Domain.Errors;

namespace Autenticacao.Domain.Models;

public class Usuario : Entity
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public DateTime DataHorarioCadastro { get; set; }
    public DateTime DataHorarioAtualizacao { get; set; }
    public DateTime? DataHorarioExclusao { get; set; }
    
    public override EntityError Validar() => new EntityError(new UsuarioValidation().Validate(this));
}