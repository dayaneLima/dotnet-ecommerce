using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Autenticacao.Application.Interfaces;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Service.Controllers;

[ApiController]
[Route("v1/autenticacao")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Autenticação")]
public class AutenticacaoController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutenticacaoController(IUsuarioService usuarioService, IHttpContextAccessor httpContextAccessor)
    {
        _usuarioService = usuarioService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AccessTokenDTO>> Autenticar([FromBody] LoginDTO login)
    {
        return await _usuarioService.AutenticarUsuario(login);
    }

    // [HttpGet]
    // [Authorize]
    // [Route("validar")]
    // public System.Security.Claims.ClaimsPrincipal Validar()
    // {
    //     return _httpContextAccessor.HttpContext!.User;
    // }
}