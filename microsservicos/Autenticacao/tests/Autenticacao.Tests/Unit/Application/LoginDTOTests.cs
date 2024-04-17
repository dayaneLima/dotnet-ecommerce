using System.ComponentModel.DataAnnotations;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Tests.Unit.Application;

public class LoginDTOTests
{
    [Fact]
    public void LoginDTO_Validar_EmailNulo_DeveRetornarErro()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = null, Senha = "senha123" };

        // Act
        var validationContext = new ValidationContext(loginDTO, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(loginDTO, validationContext, validationResults, validateAllProperties: true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email") && vr.ErrorMessage == "E-mail é obrigatório");
    }

    [Fact]
    public void LoginDTO_Validar_EmailInvalido_DeveRetornarErro()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "emailinvalido", Senha = "senha123" };

        // Act
        var validationContext = new ValidationContext(loginDTO, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(loginDTO, validationContext, validationResults, validateAllProperties: true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Email") && vr.ErrorMessage == "E-mail inválido");
    }

    [Fact]
    public void LoginDTO_Validar_SenhaNula_DeveRetornarErro()
    {
        // Arrange
        var loginDTO = new LoginDTO { Email = "test@example.com", Senha = null };

        // Act
        var validationContext = new ValidationContext(loginDTO, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(loginDTO, validationContext, validationResults, validateAllProperties: true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, vr => vr.MemberNames.Contains("Senha") && vr.ErrorMessage == "Senha é obrigatória");
    }
}