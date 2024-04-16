using Microsoft.Extensions.DependencyInjection;
using Pedidos.Domain.Repository;
using Pedidos.Data.Repository;
using Pedidos.Data.Context;
using Pedidos.Application.Interfaces;
using Pedidos.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Pedidos.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IPedidoRepository, PedidoRepository>();         
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<PedidoContext>();

        services.AddSingleton(configuration);
    }
}
