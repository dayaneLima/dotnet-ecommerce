using Pedidos.Domain.Models.Core;
using Pedidos.Domain.Validations;
using Pedidos.Domain.Errors;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Domain.Models;

public class Pedido : EntityTimestamp
{
    public double ValorTotal { get; set; }
    public StatusPedido Status { get; set; }
    public int IdUsuario { get; set; }
    public IEnumerable<ItemPedido>? ItensPedido { get; set; } //Propriedade de navegação efcore
    
    public override EntityError Validar() => new EntityError(new PedidoValidation().Validate(this));
}
