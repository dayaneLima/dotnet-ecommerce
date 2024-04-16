using FluentValidation;
using Pedidos.Domain.Models;

namespace Pedidos.Domain.Validations;

public class PedidoValidation : AbstractValidator<Pedido>
{
    public PedidoValidation()
    { 
        RuleFor(e => e.ValorTotal).NotEmpty().WithMessage("Valor total é obrigatório");
        RuleFor(e => e.Status).NotEmpty().WithMessage("Status é obrigatório");
        RuleFor(e => e.IdUsuario).NotEmpty().WithMessage("usuário é obrigatório");
    }
}
