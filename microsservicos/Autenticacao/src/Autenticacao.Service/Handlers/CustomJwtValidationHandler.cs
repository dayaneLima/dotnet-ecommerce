using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacao.Services.Handlers;

public class CustomJwtValidationHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
{
    private readonly IDistributedCache _distributedCache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomJwtValidationHandler(IDistributedCache distributedCache, IHttpContextAccessor httpContextAccessor)
    {
        _distributedCache = distributedCache;   
        _httpContextAccessor = httpContextAccessor;
    }

    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        var tokenCache = _distributedCache.GetString(token);

        if (string.IsNullOrEmpty(tokenCache))
            throw new SecurityTokenValidationException();

        return base.ValidateToken(tokenCache, validationParameters, out validatedToken);
    }
}