using Autenticacao.Application.DTOs;
using Autenticacao.Domain.Models;

namespace Autenticacao.Application.Interfaces;

public interface IUsuarioService
{
    Task<AccessTokenDTO> AutenticarUsuario(Usuario usuario);
}