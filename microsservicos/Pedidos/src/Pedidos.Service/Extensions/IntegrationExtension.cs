using Pedidos.Application.Integrations;

namespace Pedidos.Service.Extensions;

public static class IntegrationExtension
{
    public static void AddIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        IntegrationBootStrapper.RegisterIntegrations(services, configuration);
    }        
}
