using Autenticacao.Application.DTOs;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Models;

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Autenticacao.Application.Services;

public class TokenService : ITokenService
{
    private const int _HOURS_TO_EXPIRE_TOKEN = 2;

    public AccessTokenDTO GerarAccesToken(Usuario usuario)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes("MVxnN8z2HiTfLbrkVRZEK2d80KmKV4bU");

        SecurityTokenDescriptor tokenSpecificationDescriptor = DescribeTokenSpecification(usuario, key);
        SecurityToken securityToken = tokenHandler.CreateToken(tokenSpecificationDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        return new AccessTokenDTO(token);
    }

    private SecurityTokenDescriptor DescribeTokenSpecification(Usuario usuario, byte[] key)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ConfigureClaimsIdentity(usuario),
            Expires = DateTime.UtcNow.AddHours(_HOURS_TO_EXPIRE_TOKEN),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }

    private ClaimsIdentity ConfigureClaimsIdentity(Usuario usuario)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
        });

        return claimsIdentity;
    }
}