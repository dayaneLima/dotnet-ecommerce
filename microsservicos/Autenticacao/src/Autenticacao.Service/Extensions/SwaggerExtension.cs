using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace Autenticacao.Service.Extensions;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";          
        var mensagemAutenticacao = isDevelopment ? 
            "Token Dev" :
            "Autenticação baseada em Json Web Token (JWT)";

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Autenticação - API", 
                Version = "v1" ,                    
                Description = $"API para autenticação de usuários"
            });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = mensagemAutenticacao,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" 
                        }
                    },
                    new string[] { } 
                } 
            });

            c.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                    return new[] { api.GroupName };

                var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)                    
                    return new[] { controllerActionDescriptor.ControllerName };                    

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });

            c.DocInclusionPredicate((name, api) => true);
        });  
    }
}
