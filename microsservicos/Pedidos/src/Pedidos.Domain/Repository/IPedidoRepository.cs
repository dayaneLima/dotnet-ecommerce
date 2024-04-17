using Pedidos.Domain.Models;
using Pedidos.Domain.Repository.Core;

namespace Pedidos.Domain.Repository;

public interface IPedidoRepository: IRepository<Pedido> 
{
    Task<Pedido?> ObterPedidoUsuario(int idUsuario, int id);
    Task<Pedido?> ObterPedidoUsuarioComItensPedido(int idUsuario, int id);
    Task<IEnumerable<Pedido>> ObterTodosPedidosUsuario(int idUsuario);
    Task<IEnumerable<Pedido>> ObterTodosPedidosUsuarioComItensPedido(int idUsuario);
}