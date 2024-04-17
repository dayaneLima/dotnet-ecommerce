using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Produtos.Domain.Repository;
using Produtos.Data.Repository;
using Produtos.Data.Context;
using Produtos.Application.Interfaces;
using Produtos.Application.Services;

namespace Produtos.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IProdutoRepository, ProdutoRepository>();         
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ProdutoContext>();

        services.AddSingleton(configuration);
    }
}
