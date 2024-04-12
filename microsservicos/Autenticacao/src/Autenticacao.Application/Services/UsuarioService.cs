using System;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Repository;
using Autenticacao.Domain.Models;
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

    public async Task<AccessTokenDTO> AutenticarUsuario(Usuario usuario)
    {
        var usuarioAutenticado = await _usuarioRepository.ObterPorEmailSenha(usuario.Email, usuario.Senha);

        if (usuarioAutenticado == null)
            throw new AuthException("E-mail ou senha incorretos");

        return _tokenService.GerarAccesToken(usuarioAutenticado);
    }
}
  