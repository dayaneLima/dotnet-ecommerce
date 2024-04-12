using Microsoft.AspNetCore.Builder;
using Autenticacao.Service.Middlewares;

namespace Autenticacao.Service.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }        
}