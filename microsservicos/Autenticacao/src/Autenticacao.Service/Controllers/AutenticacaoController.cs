using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Autenticacao.Application.Interfaces;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Service.Controllers;

[ApiController]
[Route("v1/autenticacao")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "Autenticação")]
public class AutenticacaoController(IUsuarioService usuarioService, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AccessTokenDTO>> Autenticar([FromBody] LoginDTO login)
    {
        return await _usuarioService.AutenticarUsuario(login);
    }
}