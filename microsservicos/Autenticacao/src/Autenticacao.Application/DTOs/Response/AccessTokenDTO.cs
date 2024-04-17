namespace Autenticacao.Application.DTOs;

public record AccessTokenDTO(string Token, string TokenType, UsuarioDTO Usuario);