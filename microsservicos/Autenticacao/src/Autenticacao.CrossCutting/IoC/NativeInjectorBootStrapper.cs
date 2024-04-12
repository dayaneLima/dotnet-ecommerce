using Microsoft.Extensions.DependencyInjection;
using Autenticacao.Domain.Repository;
using Autenticacao.Data.Repository;
using Autenticacao.Data.Context;
using Autenticacao.Application.Interfaces;
using Autenticacao.Application.Services;

namespace Autenticacao.CrossCutting.IoC;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {        
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();         
        services.AddScoped<IUsuarioService, UsuarioService>(); 
        services.AddScoped<ITokenService, TokenService>(); 
        services.AddScoped<AutenticacaoContext>();
    }
}
