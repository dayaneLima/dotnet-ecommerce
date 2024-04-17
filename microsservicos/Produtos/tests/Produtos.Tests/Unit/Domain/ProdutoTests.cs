using FluentValidation.TestHelper;
using Produtos.Domain.Models;
using Produtos.Domain.Validations;

namespace Produtos.Tests.Unit.Domain;

public class ProdutoTests
{
    [Fact]
    public void Produto_DeveTerNome()
    {
        // Arrange
        var produto = new Produto { Nome = null };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Nome)
              .WithErrorMessage("Nome é obrigatório");
    }

    [Fact]
    public void Produto_NomeDeveTerNoMaximo255Caracteres()
    {
        // Arrange
        var produto = new Produto { Nome = new string('a', 256) };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Nome)
              .WithErrorMessage("Nome não pode passar de 255 caracteres");
    }

    [Fact]
    public void Produto_DescricaoDeveSerObrigatorio()
    {
        // Arrange
        var produto = new Produto { Descricao = null };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Descricao)
            .WithErrorMessage("Descrição é obrigatório");
    }

    [Fact]
    public void Produto_DescricaoDeveTerNoMaximo255Caracteres()
    {
        // Arrange
        var produto = new Produto { Descricao = new string('a', 256) }; // 256 caracteres
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Descricao)
            .WithErrorMessage("Descrição não pode passar de 255 caracteres");
    }

    [Fact]
    public void Produto_DeveTerUrlImagemValida()
    {
        // Arrange
        var produto = new Produto { UrlImagem = "invalid-url" };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.UrlImagem)
            .WithErrorMessage("A URL da imagem fornecida não é válida.");
    }

    [Fact]
    public void Produto_QuantidadeDisponivelDeveSerMaiorQueZero()
    {
        // Arrange
        var produto = new Produto { QuantidadeDisponivel = 0 };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.QuantidadeDisponivel)
            .WithErrorMessage("Quantidade deve ser maior que zero");
    }

    [Fact]
    public void Produto_QuantidadeDisponivelDeveSerObrigatorio()
    {
        // Arrange
        var produto = new Produto { QuantidadeDisponivel = 0 };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.QuantidadeDisponivel)
            .WithErrorMessage("Quantidade disponível é obrigatório");
    }

    [Fact]
    public void Produto_ValorDeveSerMaiorQueZero()
    {
        // Arrange
        var produto = new Produto { Valor = 0 };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Valor)
            .WithErrorMessage("Valor deve ser maior que zero");
    }

    [Fact]
    public void Produto_ValorDeveSerObrigatorio()
    {
        // Arrange
        var produto = new Produto { Valor = 0 };
        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Valor)
            .WithErrorMessage("Valor é obrigatório");
    }

    [Fact]
    public void Produto_DeveSerValido()
    {
        // Arrange
        var produto = new Produto
        {
            Nome = "Produto",
            Descricao = "Descrição do produto",
            Valor = 10.0,
            Categoria = "Categoria",
            QuantidadeDisponivel = 5,
            UrlImagem = "https://example.com/image.jpg"
        };

        var validator = new ProdutoValidation();

        // Act
        var result = validator.TestValidate(produto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
