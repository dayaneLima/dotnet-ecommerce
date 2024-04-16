using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Pedidos.Application.Integrations;

public class IntegrationBootStrapper
{
    public static void RegisterIntegrations(IServiceCollection services, IConfiguration configuration)
    {
        // RestService.For<IProdutoIntegrationService>(new HttpClient(HttpClientHandlerIgnoreCertificate())
        // {
        //     BaseAddress = new Uri(configuration["Integrations:Produto:Url"]!)
        // });

        // services.AddRefitClient<IProdutoIntegrationService>().ConfigureHttpClient(c =>
        // {
        //     c.BaseAddress = new  Uri(configuration["Integrations:Produto:Url"]!);
        // });
    }

    public static HttpClientHandler HttpClientHandlerIgnoreCertificate()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        return clientHandler;
    }
}
