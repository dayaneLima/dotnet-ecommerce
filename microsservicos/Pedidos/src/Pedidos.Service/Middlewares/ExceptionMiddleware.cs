using System.Net;

using Pedidos.Application.DTOs;
using Pedidos.Domain.Exceptions;

namespace Pedidos.Service.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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
        var errorDTO = new ErrorDTO() { Message = exception.Message };

        if (exception is EntityErrorException) 
        {
            statusCode = (int) HttpStatusCode.BadRequest;
            errorDTO.Descriptions = ((EntityErrorException) exception).EntityError!.Descriptions;
        }

        if (exception is HttpErrorException) 
            statusCode = ((HttpErrorException) exception).StatusCode;

        if (exception is AuthException) 
            statusCode = (int) HttpStatusCode.Unauthorized;

        if (exception is NotFoundException) 
            statusCode = (int) HttpStatusCode.NotFound;

        if (exception is BadRequestException) 
            statusCode = (int) HttpStatusCode.BadRequest;

        errorDTO.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(errorDTO.ToString());
    }
}