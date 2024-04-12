using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Autenticacao.Application.Interfaces;
using Autenticacao.Domain.Models;
using Autenticacao.Application.DTOs;

namespace Autenticacao.Service.Controllers;

[ApiController]
[Route("v1/autenticacao")]
public class AutenticacaoController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AutenticacaoController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Autenticar([FromBody] LoginDTO login)
    {
        return await _usuarioService.AutenticarUsuario(new Usuario() {Senha= login.Senha, Email= login.Email});
    }

    [HttpGet]
    [Authorize]
    [Route("authenticated")]
    public string Authenticated() => String.Format("Authenticated - {0}", "asdas");
    // public string Authenticated() => String.Format("Authenticated - {0}", User?.Identity?.Name);
}