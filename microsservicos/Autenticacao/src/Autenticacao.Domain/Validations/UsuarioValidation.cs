using FluentValidation;
using Autenticacao.Domain.Models;

namespace Autenticacao.Domain.Validations;

public class UsuarioValidation : AbstractValidator<Usuario>
{
    public UsuarioValidation()
    {             
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(255).WithMessage("Nome não pode passar de 255 caracteres");

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .EmailAddress().WithMessage("E-mail não é válido")
            .MaximumLength(255).WithMessage("E-mail não pode passar de 255 caracteres");

        RuleFor(e => e.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres")
            .MaximumLength(255).WithMessage("Senha não pode passar de 255 caracteres")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$").WithMessage("A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número");
    }
}
