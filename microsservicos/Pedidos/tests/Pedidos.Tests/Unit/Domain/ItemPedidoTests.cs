using FluentValidation.TestHelper;
using Pedidos.Domain.Models;
using Pedidos.Domain.Validations;

namespace Pedidos.Tests.Unit.Domain;

public class ItemPedidoTests
{
    [Fact]
    public void Validar_DeveRetornarErroQuandoQuantidadeNaoEhInformada()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 0, PrecoVenda = 100, IdProduto = 1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Quantidade)
            .WithErrorMessage("Quantidade é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoQuantidadeEhMenorOuIgualAZero()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = -10, PrecoVenda = 100, IdProduto = 1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Quantidade)
            .WithErrorMessage("Quantidade deve ser maior que zero");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoPrecoVendaNaoEhInformado()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 10, PrecoVenda = 0, IdProduto = 1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.PrecoVenda)
            .WithErrorMessage("Preço de venda é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoPrecoVendaEhMenorOuIgualAZero()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 10, PrecoVenda = -100, IdProduto = 1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.PrecoVenda)
            .WithErrorMessage("Preço de venda deve ser maior que zero");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoIdProdutoNaoEhInformado()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 10, PrecoVenda = 100, IdProduto = 0 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.IdProduto)
            .WithErrorMessage("Produto é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoIdProdutoEhMenorOuIgualAZero()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 10, PrecoVenda = 100, IdProduto = -1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.IdProduto)
            .WithErrorMessage("Identificador do produto deve ser maior que zero");
    }

    [Fact]
    public void Validar_NaoDeveRetornarErroQuandoTodosOsCamposEstaoCorretos()
    {
        // Arrange
        var itemPedido = new ItemPedido { Quantidade = 10, PrecoVenda = 100, IdProduto = 1 };

        // Act
        var result = new ItemPedidoValidation().TestValidate(itemPedido);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.Quantidade);
        result.ShouldNotHaveValidationErrorFor(p => p.PrecoVenda);
        result.ShouldNotHaveValidationErrorFor(p => p.IdProduto);
    }
}
