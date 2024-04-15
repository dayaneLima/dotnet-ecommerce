using AutoMapper;

namespace Autenticacao.Application.AutoMappers;

public class AutoMapperMappingProfile : Profile 
{
    public AutoMapperMappingProfile()
    {
        new UsuarioParaUsuarioDTO(this);
    }
}