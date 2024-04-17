using Pedidos.Application.DTOs;
using Pedidos.Domain.Models;

namespace Pedidos.Application.AutoMappers;

public class PedidoParaPedidoRetornoDTO
{
    public PedidoParaPedidoRetornoDTO(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<Pedido, PedidoRetornoDTO>()
            .ForMember(p => p.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}