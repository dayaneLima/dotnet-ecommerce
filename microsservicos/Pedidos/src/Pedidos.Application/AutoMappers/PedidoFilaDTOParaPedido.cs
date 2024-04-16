using Pedidos.Application.DTOs;
using Pedidos.Domain.Models;

namespace Pedidos.Application.AutoMappers;

public class PedidoFilaDTOParaPedido
{
    public PedidoFilaDTOParaPedido(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<PedidoFilaDTO, Pedido>();
    }
}