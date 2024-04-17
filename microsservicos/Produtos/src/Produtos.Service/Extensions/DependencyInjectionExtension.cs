using Produtos.CrossCutting.IoC;

namespace Produtos.Service.Extensions;

public static class DependencyInjection
{
    public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        NativeInjectorBootStrapper.RegisterServices(services, configuration);
    }        
}
