using Autenticacao.Application.DTOs;

namespace Autenticacao.Application.Interfaces;

public interface IUsuarioService
{
    Task<AccessTokenDTO> AutenticarUsuario(LoginDTO login);
}