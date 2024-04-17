namespace Pedidos.Application.MessageBus;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}