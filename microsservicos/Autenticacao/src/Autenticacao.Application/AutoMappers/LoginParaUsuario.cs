using Autenticacao.Application.AutoMappers;
using Autenticacao.Application.DTOs;
using Autenticacao.Domain.Models;

namespace Autenticacao.Application.AutoMappers;

public class LoginParaUsuario
{
    public LoginParaUsuario(AutoMapperMappingProfile mapping) 
    {
        mapping.CreateMap<LoginDTO, Usuario>();
    }
}