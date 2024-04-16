using Microsoft.AspNetCore.Builder;
using Pedidos.Service.Middlewares;

namespace Pedidos.Service.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }        
}