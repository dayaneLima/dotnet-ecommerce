using Pedidos.Domain.Models.Core;
using Pedidos.Domain.Validations;
using Pedidos.Domain.Errors;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Domain.Models;

public class Pedido : Entity
{
    public double ValorTotal { get; set; }
    public StatusPedido Status { get; set; }
    public int IdUsuario { get; set; }
    
    public DateTime? DataHorarioCadastro { get; set; }
    public DateTime? DataHorarioAtualizacao { get; set; }
    public DateTime? DataHorarioExclusao { get; set; }

    public IEnumerable<ItemPedido>? ItensPedido { get; set; } //Propriedade de navegação efcore
    
    public override EntityError Validar() => new EntityError(new PedidoValidation().Validate(this));
}
