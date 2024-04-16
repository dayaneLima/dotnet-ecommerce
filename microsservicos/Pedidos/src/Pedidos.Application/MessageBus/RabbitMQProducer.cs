using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Pedidos.Application.DTOs;
using RabbitMQ.Client;

namespace Pedidos.Application.MessageBus;

public class RabbitMQProducer(IConfiguration configuration) : IMessageProducer
{
    private readonly IConfiguration _configuration = configuration;

    public void SendMessage(PedidoFilaDTO message)
    {
        var connection =  CreateConnection();

        using  var channel = connection.CreateModel();
        channel.QueueDeclare(_configuration["RabbitMQ:Queues:Pedido"], false, false, false, arguments: null);

        var body = SerializeMessage(message);

        channel.BasicPublish(exchange: "", routingKey: _configuration["RabbitMQ:Queues:Pedido"], body: body);
    }

    private IConnection CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"],
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = _configuration["RabbitMQ:User"],
                Password = _configuration["RabbitMQ:Password"]
            };

            return factory.CreateConnection();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private byte[] SerializeMessage(PedidoFilaDTO message)
    { 
        var json = JsonSerializer.Serialize<PedidoFilaDTO>(message);
        return Encoding.UTF8.GetBytes(json);
    }
}