using Pedidos.Application.DTOs;

namespace Pedidos.Application.AutoMappers;

public class PedidoDTOParaPedidoFilaDTO
{
    public PedidoDTOParaPedidoFilaDTO(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<PedidoDTO, PedidoFilaDTO>();
    }
}