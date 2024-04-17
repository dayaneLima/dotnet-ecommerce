using System.Net;

using Produtos.Application.DTOs;
using Produtos.Domain.Exceptions;

namespace Produtos.Service.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = (int) HttpStatusCode.InternalServerError;
        var errorDTO = new ErrorDTO { Message = exception.Message };

        statusCode = exception switch
        {
            EntityErrorException entityError => HandleEntityError(entityError, errorDTO),
            HttpErrorException httpError => httpError.StatusCode,
            AuthException => (int) HttpStatusCode.Unauthorized,
            NotFoundException => (int) HttpStatusCode.NotFound,
            BadRequestException => (int) HttpStatusCode.BadRequest,
            _ => statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(errorDTO.ToString());
    }

    private static int HandleEntityError(EntityErrorException entityError, ErrorDTO errorDTO)
    {
        errorDTO.Descriptions = entityError.EntityError!.Descriptions;
        return (int) HttpStatusCode.BadRequest;
    }
}