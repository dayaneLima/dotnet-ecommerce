using Moq;
using Autenticacao.Domain.Repository;
using Autenticacao.Application.DTOs;
using Autenticacao.Domain.Models;
using Autenticacao.Application.Interfaces;
using Autenticacao.Application.Services;
using Autenticacao.Domain.Exceptions;

namespace Autenticacao.Tests.Unit.Application;

public class UsuarioServiceTests
{
    [Fact]
    public async Task AutenticarUsuario_DeveRetornarAccessTokenQuandoCredenciaisCorretas()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "test@example.com", Senha = "senha123" };

        var usuarioAutenticado = new Usuario { Id = 1, Nome = "Teste", Email = "test@example.com", Senha = BCrypt.Net.BCrypt.HashPassword("senha123") };
        var usuarioDTO = new UsuarioDTO(1, "Teste");
        var accessTokenDTO = new AccessTokenDTO("token", "Bearer", usuarioDTO);

        var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        usuarioRepositoryMock.Setup(repo => repo.ObterPorEmail(loginDTO.Email)).ReturnsAsync(usuarioAutenticado);

        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(service => service.GerarAccesToken(usuarioAutenticado)).Returns(accessTokenDTO);

        var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await usuarioService.AutenticarUsuario(loginDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accessTokenDTO.Token, result.Token);
        Assert.Equal(accessTokenDTO.TokenType, result.TokenType);
    }

    [Fact]
    public async Task AutenticarUsuario_DeveLancarAuthExceptionQuandoCredenciaisIncorretas()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "test@example.com", Senha = "senha123" };
        var mensagemEsperada = "E-mail ou senha incorretos";

        var usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        usuarioRepositoryMock.Setup(repo => repo.ObterPorEmail(loginDTO.Email)).ReturnsAsync((Usuario) null);

        var tokenServiceMock = new Mock<ITokenService>();

        var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, tokenServiceMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AuthException>(() => usuarioService.AutenticarUsuario(loginDTO));
        Assert.Equal(mensagemEsperada, exception.Message);

    }
}