using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Produtos.Service.Extensions;

public static class AutenticationJwtExtension
{
    public static void AddAutenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {

        AuthenticationBuilder authenticationBuilder = services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        authenticationBuilder.ConfigureJwtBearer(configuration);
    }

    private static void ConfigureJwtBearer(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
    {
        authenticationBuilder.AddJwtBearer(jwt => 
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"] ?? "")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}
