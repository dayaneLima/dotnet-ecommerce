using Moq;
using AutoMapper;

using Pedidos.Application.Services;
using Pedidos.Application.DTOs;
using Pedidos.Application.MessageBus;
using Pedidos.Domain.Repository;
using Pedidos.Application.Integrations;

namespace Pedidos.Tests.Unit.Application;

public class PedidoServiceTests
{
    [Fact]
    public void PublicarNaFila_DeveChamarMessageProducerComPedidoFilaDTO()
    {
        // Arrange
        var messageProducerMock = new Mock<IMessageProducer>();
        var mapperMock = new Mock<IMapper>();
        var pedidoRepositoryMock = new Mock<IPedidoRepository>();
        var produtoIntegrationServiceMock = new Mock<IProdutoIntegrationService>();
        var pedidoService = new PedidoService(messageProducerMock.Object, mapperMock.Object, pedidoRepositoryMock.Object, produtoIntegrationServiceMock.Object);

        var pedidoDTO = new PedidoDTO();
        var idUsuario = 1;
        var pedidoFilaDTO = new PedidoFilaDTO(){IdUsuario = idUsuario};

        mapperMock.Setup(m => m.Map<PedidoFilaDTO>(pedidoDTO)).Returns(pedidoFilaDTO);
        messageProducerMock.Setup(m => m.SendMessage<PedidoFilaDTO>(pedidoFilaDTO));

        // Act
        pedidoService.PublicarNaFila(idUsuario, pedidoDTO);

        // Assert
        messageProducerMock.Verify(mp => mp.SendMessage(It.IsAny<PedidoFilaDTO>()), Times.Once);
    }
}
