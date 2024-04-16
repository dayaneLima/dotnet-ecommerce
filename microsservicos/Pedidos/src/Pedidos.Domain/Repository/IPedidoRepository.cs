using Pedidos.Domain.Models;
using Pedidos.Domain.Repository.Core;

namespace Pedidos.Domain.Repository;

public interface IPedidoRepository: IRepository<Pedido> { }