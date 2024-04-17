using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Repository;
using Autenticacao.Domain.Exceptions;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Application.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService) : IUsuarioService
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<AccessTokenDTO> AutenticarUsuario(LoginDTO loginDTO)
    {
        var usuarioAutenticado = await _usuarioRepository.ObterPorEmail(loginDTO.Email!);

        if (usuarioAutenticado is null || !BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuarioAutenticado.Senha))
            throw new AuthException("E-mail ou senha incorretos");

        return _tokenService.GerarAccesToken(usuarioAutenticado!);
    }
}
  