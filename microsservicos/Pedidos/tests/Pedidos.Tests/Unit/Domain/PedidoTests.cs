using Pedidos.Domain.Models;
using Pedidos.Domain.Validations;
using Pedidos.Domain.ValueObjects;

using FluentValidation.TestHelper;

namespace Pedidos.Tests.Unit.Domain;

public class PedidoTests
{
    [Fact]
    public void Validar_DeveRetornarErroQuandoValorTotalNaoEhInformado()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 0, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 1 };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ValorTotal)
            .WithErrorMessage("Valor total é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoValorTotalEhMenorOuIgualAZero()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = -10, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 1 };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ValorTotal)
            .WithErrorMessage("Valor total deve ser maior que zero");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoStatusNaoEhValido()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = (StatusPedido)100, IdUsuario = 1 };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.Status)
            .WithErrorMessage("O valor status não é válido");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoIdUsuarioNaoEhInformado()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 0 };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.IdUsuario)
            .WithErrorMessage("Usuário é obrigatório");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoIdUsuarioEhMenorOuIgualAZero()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = -1 };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.IdUsuario)
            .WithErrorMessage("Identificador do usuário deve ser maior que zero");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoItensPedidoNaoEhInformado()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 1, ItensPedido = null };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ItensPedido)
            .WithErrorMessage("O pedido deve ter ao menos um item");
    }

    [Fact]
    public void Validar_DeveRetornarErroQuandoItensPedidoEhVazio()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 1, ItensPedido = new List<ItemPedido>() };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldHaveValidationErrorFor(p => p.ItensPedido)
            .WithErrorMessage("O pedido deve ter ao menos um item");
    }

    [Fact]
    public void Validar_NaoDeveRetornarErroQuandoTodosOsCamposEstaoCorretos()
    {
        // Arrange
        var pedido = new Pedido { ValorTotal = 100, Status = StatusPedido.EM_PROCESSAMENTO, IdUsuario = 1, ItensPedido = new List<ItemPedido> { new ItemPedido() } };

        // Act
        var result = new PedidoValidation().TestValidate(pedido);

        // Assert
        result.ShouldNotHaveValidationErrorFor(p => p.ValorTotal);
        result.ShouldNotHaveValidationErrorFor(p => p.Status);
        result.ShouldNotHaveValidationErrorFor(p => p.IdUsuario);
        result.ShouldNotHaveValidationErrorFor(p => p.ItensPedido);
    }
}
