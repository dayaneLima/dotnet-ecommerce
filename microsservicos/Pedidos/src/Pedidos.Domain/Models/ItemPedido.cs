using Pedidos.Domain.Models.Core;
using Pedidos.Domain.Validations;
using Pedidos.Domain.Errors;

namespace Pedidos.Domain.Models;

public class ItemPedido : EntityTimestamp
{
    public int Quantidade { get; set; }
    public double PrecoVenda { get; set; }
    public int IdProduto { get; set; }
    public Pedido? Pedido { get; set; } //Propriedade de navegação efcore
    
    public override EntityError Validar() => new EntityError(new ItemPedidoValidation().Validate(this));
}
