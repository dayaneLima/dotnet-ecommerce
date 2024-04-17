using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Autenticacao.Domain.Repository;
using Autenticacao.Data.Repository;
using Autenticacao.Data.Context;
using Autenticacao.Application.Interfaces;
using Autenticacao.Application.Services;

namespace Autenticacao.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();         
        services.AddScoped<IUsuarioService, UsuarioService>(); 
        services.AddScoped<ITokenService, TokenService>(); 
        services.AddScoped<AutenticacaoContext>();

        services.AddSingleton(configuration);
    }
}
