using FluentValidation;
using Pedidos.Domain.Models;

namespace Pedidos.Domain.Validations;

public class PedidoValidation : AbstractValidator<Pedido>
{
    public PedidoValidation()
    { 
        RuleFor(e => e.ValorTotal)
            .NotEmpty().WithMessage("Valor total é obrigatório")
            .GreaterThan(0).WithMessage("Valor total deve ser maior que zero");

        RuleFor(e => e.Status).IsInEnum().WithMessage("O valor status não é válido");

        RuleFor(e => e.IdUsuario)
            .NotEmpty().WithMessage("Usuário é obrigatório")
            .GreaterThan(0).WithMessage("Identificador do usuário deve ser maior que zero");

        RuleFor(e => e.ItensPedido).NotEmpty().WithMessage("O pedido deve ter ao menos um item");
    }
}
