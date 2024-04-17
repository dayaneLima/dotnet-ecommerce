using Produtos.Service.Middlewares;

namespace Produtos.Service.Extensions;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }        
}