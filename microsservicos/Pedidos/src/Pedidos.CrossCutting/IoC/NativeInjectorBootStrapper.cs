using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Pedidos.Application.MessageBus;
using Pedidos.Application.Integrations;
using Pedidos.Domain.Repository;
using Pedidos.Data.Repository;
using Pedidos.Data.Context;
using Pedidos.Application.Interfaces;
using Pedidos.Application.Services;

namespace Pedidos.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IPedidoRepository, PedidoRepository>();         
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IMessageProducer, RabbitMQProducer>();

        var produtoIntegrationService =  Refit.RestService.For<IProdutoIntegrationService>(new HttpClient(HttpClientHandlerIgnoreCertificate())
        {
            BaseAddress = new Uri(configuration["Integrations:Produto:Url"]!)
        });
        
        services.AddScoped<IProdutoIntegrationService>(x => produtoIntegrationService);

        services.AddScoped<PedidoContext>();

        services.AddSingleton(configuration);
    }

    private static HttpClientHandler HttpClientHandlerIgnoreCertificate()
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };
    }
}
