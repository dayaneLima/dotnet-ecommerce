using Autenticacao.Domain.Models;

namespace Autenticacao.Tests.Unit.Domain;

public class UsuarioTests
{
    [Fact]
    public void Validar_DeveRetornarErroQuandoNomeNulo()
    {
        // Arrange
        var usuario = new Usuario { Nome = null, Email = "test@example.com", Senha = "Teste1" };

        // Act
        var result = usuario.Validar();

        // Assert
        Assert.Contains(result.Descriptions, d => d.Description == "Nome é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoEmailNuloOuInvalido()
    {
        // Arrange
        var usuario1 = new Usuario { Nome = "Teste", Email = null, Senha = "Teste1" };
        var usuario2 = new Usuario { Nome = "Teste", Email = "invalid-email", Senha = "Teste1" };

        // Act
        var result1 = usuario1.Validar();
        var result2 = usuario2.Validar();

        // Assert
        Assert.Contains(result1.Descriptions, d => d.Description == "E-mail é obrigatório");
        Assert.Contains(result2.Descriptions, d => d.Description == "E-mail não é válido");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoSenhaNulaOuMenorQueSeisCaracteresOuFraca()
    {
        // Arrange
        var usuario1 = new Usuario { Nome = "Teste", Email = "test@example.com", Senha = null };
        var usuario2 = new Usuario { Nome = "Teste", Email = "test@example.com", Senha = "12345" };
        var usuario3 = new Usuario { Nome = "Teste", Email = "test@example.com", Senha = "123456" };

        // Act
        var result1 = usuario1.Validar();
        var result2 = usuario2.Validar();
        var result3 = usuario3.Validar();

        // Assert
        Assert.Contains(result1.Descriptions, d => d.Description == "Senha é obrigatória");
        Assert.Contains(result2.Descriptions, d => d.Description == "Senha deve ter no mínimo 6 caracteres");
        Assert.Contains(result3.Descriptions, d => d.Description == "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número");
    }
}
