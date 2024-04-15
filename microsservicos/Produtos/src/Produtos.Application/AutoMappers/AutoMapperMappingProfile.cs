using AutoMapper;

namespace Produtos.Application.AutoMappers;

public class AutoMapperMappingProfile : Profile 
{
    public AutoMapperMappingProfile()
    {
        new ProdutoDTOParaProduto(this);
        new ProdutoParaProdutoRetornoDTO(this);
    }
}