using System.ComponentModel.DataAnnotations;
using Produtos.Application.DTOs;

namespace Produtos.Tests.Unit.Application;

public class ProdutoDTOTests
{
    [Fact]
    public void ProdutoDTO_DeveTerNome()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { Nome = null };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "Nome é obrigatório");
    }

    [Fact]
    public void ProdutoDTO_DeveTerDescricao()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { Descricao = null };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "Descrição é obrigatória");
    }

    [Fact]
    public void ProdutoDTO_DeveTerValorMaiorQueZero()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { Valor = 0 };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "O valor do produto deve ser maior que zero");
    }

    [Fact]
    public void ProdutoDTO_CategoriaDeveSerObrigatorio()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { Categoria = null };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "Categoria é obrigatória");
    }

    [Fact]
    public void ProdutoDTO_CategoriaDeveTerNoMaximo255Caracteres()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { Categoria = new string('a', 256) }; // 256 caracteres
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A categoria não pode passar de 255 caracteres");
    }

    [Fact]
    public void ProdutoDTO_UrlImagemDeveSerObrigatorio()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { UrlImagem = null };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "UrlImagem é obrigatória");
    }

    [Fact]
    public void ProdutoDTO_UrlImagemNaoDeveSerValida()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { UrlImagem = "invalid-url" };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A URL fornecida não é válida.");
    }

    [Theory]
    [InlineData("http://example.com/image.jpg")]
    [InlineData("https://example.com/image.jpg")]
    public void ProdutoDTO_UrlImagemDeveSerValida(string url)
    {
        // Arrange
        var produtoDTO = new ProdutoDTO
        {
            Nome = "Produto",
            Descricao = "Descrição do produto",
            Valor = 10.0,
            Categoria = "Categoria",
            QuantidadeDisponivel = 5,
            UrlImagem = url
        };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }

    [Fact]
    public void ProdutoDTO_UrlImagemDeveTerNoMaximo255Caracteres()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { UrlImagem = new string('a', 256) }; // 256 caracteres
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A url não pode passar de 255 caracteres");
    }

    [Fact]
    public void ProdutoDTO_QuantidadeDisponivelNaoPodeSerNegativa()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO { QuantidadeDisponivel = -1 };
        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "A quantidade deve ser um valor positivo");
    }

    [Fact]
    public void ProdutoDTO_DeveSerValido()
    {
        // Arrange
        var produtoDTO = new ProdutoDTO
        {
            Nome = "Produto",
            Descricao = "Descrição do produto",
            Valor = 10.0,
            Categoria = "Categoria",
            QuantidadeDisponivel = 5,
            UrlImagem = "https://example.com/image.jpg"
        };

        var context = new ValidationContext(produtoDTO);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(produtoDTO, context, results, true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}
