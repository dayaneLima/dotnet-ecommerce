using AutoMapper;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;

using Autenticacao.Application.DTOs;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Models;

namespace Autenticacao.Application.Services;

public class TokenService(IConfiguration configuration, IMapper mapper, IDistributedCache distributedCache) : ITokenService
{
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _configuration = configuration;
    private readonly IDistributedCache _distributedCache = distributedCache;

    public AccessTokenDTO GerarAccesToken(Usuario usuario)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!);

        SecurityTokenDescriptor tokenSpecificationDescriptor = DescribeTokenSpecification(usuario, key);
        SecurityToken securityToken = tokenHandler.CreateToken(tokenSpecificationDescriptor);
        string token = tokenHandler.WriteToken(securityToken);

        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

        _distributedCache.SetString(token, JsonSerializer.Serialize(usuarioDTO), new DistributedCacheEntryOptions() {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(int.Parse(_configuration["JWT:HoursToExpireToken"]!))
        });

        return new AccessTokenDTO(token, "Bearer", usuarioDTO);
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
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome!.ToString()),
        });

        return claimsIdentity;
    }
}