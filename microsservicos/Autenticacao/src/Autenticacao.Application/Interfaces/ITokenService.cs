using Autenticacao.Application.DTOs;
using Autenticacao.Domain.Models;

namespace Autenticacao.Application.Interfaces;
public interface ITokenService
{
    AccessTokenDTO GerarAccesToken(Usuario usuario);
}