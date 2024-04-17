using Autenticacao.Application.DTOs;
using Autenticacao.Domain.Models;

namespace Autenticacao.Application.AutoMappers;

public class UsuarioParaUsuarioDTO
{
    public UsuarioParaUsuarioDTO(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<Usuario, UsuarioDTO>();
    }
}