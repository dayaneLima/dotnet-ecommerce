using Pedidos.Domain.Models;
using Pedidos.Domain.Repository;
using Pedidos.Data.Context;

namespace Pedidos.Data.Repository;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(PedidoContext context) : base(context) { }
}