using Produtos.Application.DTOs;
using Produtos.Domain.Models;

namespace Produtos.Application.AutoMappers;

public class ProdutoParaProdutoRetornoDTO
{
    public ProdutoParaProdutoRetornoDTO(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<Produto, ProdutoRetornoDTO>();
    }
}