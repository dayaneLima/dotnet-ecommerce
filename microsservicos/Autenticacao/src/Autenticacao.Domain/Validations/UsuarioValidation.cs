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

        RuleFor(e => e.Email).NotEmpty().WithMessage("E-mail é obrigatório");

        RuleFor(e => e.Senha).NotEmpty().WithMessage("Senha é obrigatória");
    }
}
