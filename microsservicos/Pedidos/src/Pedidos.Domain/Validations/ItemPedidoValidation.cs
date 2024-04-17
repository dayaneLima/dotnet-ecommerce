using FluentValidation;
using Pedidos.Domain.Models;

namespace Pedidos.Domain.Validations;

public class ItemPedidoValidation : AbstractValidator<ItemPedido>
{
    public ItemPedidoValidation()
    {     
        RuleFor(e => e.Quantidade)
            .NotEmpty().WithMessage("Quantidade é obrigatório")
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero");  

        RuleFor(e => e.PrecoVenda)
            .NotEmpty().WithMessage("Preço de venda é obrigatório")
            .GreaterThan(0).WithMessage("Preço de venda deve ser maior que zero");  

        RuleFor(e => e.IdProduto)
            .NotEmpty().WithMessage("Produto é obrigatório")
            .GreaterThan(0).WithMessage("Identificador do produto deve ser maior que zero");
    }
}
