using AutoMapper;

namespace Pedidos.Application.AutoMappers;

public class AutoMapperMappingProfile : Profile 
{
    public AutoMapperMappingProfile()
    {
        new PedidoDTOParaPedidoFilaDTO(this);
        new PedidoFilaDTOParaPedido(this);
        new ItemPedidoDTOParaItemPedido(this);
    }
}