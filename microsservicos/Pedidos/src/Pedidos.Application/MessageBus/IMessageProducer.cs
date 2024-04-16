using Pedidos.Application.DTOs;

namespace Pedidos.Application.MessageBus;

public interface IMessageProducer
{
    void SendMessage(PedidoFilaDTO message);
}