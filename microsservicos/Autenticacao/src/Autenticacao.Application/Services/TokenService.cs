using Autenticacao.Application.DTOs;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Models;

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

namespace Autenticacao.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _distributedCache;

    public TokenService(IConfiguration configuration, IDistributedCache distributedCache)
    {
        _configuration = configuration;
        _distributedCache = distributedCache;
    }

    public AccessTokenDTO GerarAccesToken(Usuario usuario)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]  ?? "");

        SecurityTokenDescriptor tokenSpecificationDescriptor = DescribeTokenSpecification(usuario, key);
        SecurityToken securityToken = tokenHandler.CreateToken(tokenSpecificationDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        _distributedCache.SetString(token, usuario.Email!, new DistributedCacheEntryOptions() {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(int.Parse(_configuration["JWT:HoursToExpireToken"] ?? "0"))
        });

        return new AccessTokenDTO(token);
    }

    private SecurityTokenDescriptor DescribeTokenSpecification(Usuario usuario, byte[] key)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ConfigureClaimsIdentity(usuario),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:HoursToExpireToken"] ?? "0")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }

    private ClaimsIdentity ConfigureClaimsIdentity(Usuario usuario)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome!.ToString()),
        });

        return claimsIdentity;
    }
}