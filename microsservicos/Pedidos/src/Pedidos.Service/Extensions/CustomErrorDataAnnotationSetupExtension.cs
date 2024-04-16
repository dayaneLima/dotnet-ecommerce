using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.DTOs;

namespace Pedidos.Service.Extensions;

public static class CustomErrorDataAnnotationSetupExtension
{
    public static void AddCustomizacaoErros(this IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options => {
            options.InvalidModelStateResponseFactory = context => 
            {
                var errorDTO = new ErrorDTO {
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Dados recebidos inválidos" 
                };

                string key = string.Empty;

                foreach (var modelState in context.ModelState) 
                {
                    key = modelState.Key;

                    foreach (var error in modelState.Value.Errors)
                        errorDTO.AddError(error.ErrorMessage, key);
                }

                return new BadRequestObjectResult(errorDTO);                    
            };
        });
    }
}