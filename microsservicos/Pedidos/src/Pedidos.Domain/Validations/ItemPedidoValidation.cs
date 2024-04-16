using FluentValidation;
using Pedidos.Domain.Models;

namespace Pedidos.Domain.Validations;

public class ItemPedidoValidation : AbstractValidator<ItemPedido>
{
    public ItemPedidoValidation()
    {     
        RuleFor(e => e.Quantidade).NotEmpty().WithMessage("Quantidade é obrigatório");        
        RuleFor(e => e.PrecoVenda).NotEmpty().WithMessage("Preço de venda é obrigatório");        
        RuleFor(e => e.IdProduto).NotEmpty().WithMessage("Produto é obrigatório");
    }
}
