using Pedidos.Application.DTOs;
using Pedidos.Domain.Models;

namespace Pedidos.Application.AutoMappers;

public class ItemPedidoDTOParaItemPedido
{
    public ItemPedidoDTOParaItemPedido(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<ItemPedidoDTO, ItemPedido>();
    }
}