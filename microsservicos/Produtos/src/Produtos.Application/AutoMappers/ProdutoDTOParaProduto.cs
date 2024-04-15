using Produtos.Application.DTOs;
using Produtos.Domain.Models;

namespace Produtos.Application.AutoMappers;

public class ProdutoDTOParaProduto
{
    public ProdutoDTOParaProduto(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<ProdutoDTO, Produto>();
    }
}