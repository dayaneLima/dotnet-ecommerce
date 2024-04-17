using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Pedidos.Application.DTOs;
using Pedidos.Application.Interfaces;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Application.MessageBus;

public class RabbitMQPedidoConsumer : BackgroundService
{
    private readonly string queueName;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    public readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMQPedidoConsumer(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var configuration = scope.ServiceProvider.GetService<IConfiguration>()!;            
            queueName = configuration["RabbitMQ:Queues:Pedido"]!;

            _connection = CreateConnection(configuration);
            _channel = _connection.CreateModel();
            
            _channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
       
        consumer.Received += async (chanel, evt) => {
            var pedidoFilaDTO = ObterPedidoFilaDTO(evt.Body);
            await ProcessarPedido(pedidoFilaDTO);
            _channel.BasicAck(evt.DeliveryTag, false);//retira da lista
        };

        _channel.BasicConsume(queueName, false, consumer: consumer); 

        return Task.CompletedTask;
    }

    private IConnection CreateConnection(IConfiguration configuration)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:Host"],
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = configuration["RabbitMQ:User"],
                Password = configuration["RabbitMQ:Password"]
            };

            return factory.CreateConnection();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private PedidoFilaDTO ObterPedidoFilaDTO(ReadOnlyMemory<byte> body)
    {
        var message = Encoding.UTF8.GetString(body.ToArray());
        var pedidoFilaDTO = JsonSerializer.Deserialize<PedidoFilaDTO>(message);

        if (pedidoFilaDTO is null)
            throw new NotFoundException("Pedido para processar não encontrado");

        return pedidoFilaDTO;
    }

    private async Task ProcessarPedido(PedidoFilaDTO pedidoFilaDTO)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var pedidoService = scope.ServiceProvider.GetService<IPedidoService>()!;
            await pedidoService.Inserir(pedidoFilaDTO);
        }
    }
}