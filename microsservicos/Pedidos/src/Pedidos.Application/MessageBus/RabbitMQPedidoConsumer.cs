using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pedidos.Application.DTOs;
using Pedidos.Application.Interfaces;
using Pedidos.Domain.Exceptions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Pedidos.Application.MessageBus;

public class RabbitMQPedidoConsumer : BackgroundService
{
    public readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly string fila;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQPedidoConsumer(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var configuration = scope.ServiceProvider.GetService<IConfiguration>()!;

            fila = configuration["RabbitMQ:Queues:Pedido"]!;

            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:Host"],
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = configuration["RabbitMQ:User"],
                Password = configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: fila, false, false, false, arguments: null);
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
       
        consumer.Received += async (chanel, evt) => {
            var message = Encoding.UTF8.GetString(evt.Body.ToArray());
            var pedidoFilaDTO = JsonSerializer.Deserialize<PedidoFilaDTO>(message);

            if (pedidoFilaDTO is null)
                throw new NotFoundException("Pedido para processar não encontrado");

            await ProcessarPedido(pedidoFilaDTO);
            _channel.BasicAck(evt.DeliveryTag, false);//retira da lista
        };

        _channel.BasicConsume(fila, false, consumer: consumer); 

        return Task.CompletedTask;
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