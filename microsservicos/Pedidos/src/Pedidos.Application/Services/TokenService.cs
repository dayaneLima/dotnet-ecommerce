using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using Pedidos.Application.Interfaces;

namespace Pedidos.Application.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;

    public string GerarAccesToken()
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!);

        SecurityTokenDescriptor tokenSpecificationDescriptor = DescribeTokenSpecification(key);
        SecurityToken securityToken = tokenHandler.CreateToken(tokenSpecificationDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private SecurityTokenDescriptor DescribeTokenSpecification(byte[] key)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ConfigureClaimsIdentity(),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:HoursToExpireToken"] ?? "0")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }

    private ClaimsIdentity ConfigureClaimsIdentity()
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, _configuration["JWT:NameIdentifier"]!),
            new Claim(ClaimTypes.Name, _configuration["JWT:Name"]!),
        });

        return claimsIdentity;
    }
}