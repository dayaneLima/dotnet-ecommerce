using Pedidos.Application.DTOs;
using Pedidos.Domain.Models;

namespace Pedidos.Application.AutoMappers;

public class PedidoDTOParaPedido
{
    public PedidoDTOParaPedido(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<PedidoDTO, Pedido>();
    }
}