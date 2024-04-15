using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Repository;
using Autenticacao.Domain.Exceptions;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ITokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<AccessTokenDTO> AutenticarUsuario(LoginDTO loginDTO)
    {
        var usuarioAutenticado = await _usuarioRepository.ObterPorEmail(loginDTO.Email!);

        if (usuarioAutenticado is null || !BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuarioAutenticado.Senha))
            throw new AuthException("E-mail ou senha incorretos");

        return _tokenService.GerarAccesToken(usuarioAutenticado!);
    }
}
  