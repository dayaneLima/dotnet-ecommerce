using Microsoft.EntityFrameworkCore;

using Pedidos.Domain.Models;
using Pedidos.Domain.Repository;
using Pedidos.Data.Context;

namespace Pedidos.Data.Repository;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(PedidoContext context) : base(context) { }

    public async Task<Pedido?> ObterPedidoUsuario(int idUsuario, int id)
    {
        return await _dbSet.Where(e =>  e.Id == id && e.IdUsuario == idUsuario).FirstOrDefaultAsync();
    }

    public async Task<Pedido?> ObterPedidoUsuarioComItensPedido(int idUsuario, int id)
    {
        return await _dbSet.Where(e =>  e.Id == id && e.IdUsuario == idUsuario).Include(p => p.ItensPedido).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Pedido>> ObterTodosPedidosUsuario(int idUsuario)
    {
        return await _dbSet.Where(e =>  e.IdUsuario == idUsuario).ToListAsync();
    }

    public async Task<IEnumerable<Pedido>> ObterTodosPedidosUsuarioComItensPedido(int idUsuario)
    {
        return await _dbSet.Where(e =>  e.IdUsuario == idUsuario).Include(p => p.ItensPedido).ToListAsync();
    }
}