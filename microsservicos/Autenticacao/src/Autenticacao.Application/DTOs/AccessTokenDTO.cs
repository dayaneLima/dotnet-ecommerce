namespace Autenticacao.Application.DTOs;

public class AccessTokenDTO
{
    public string Token { get; private set; }
    public string Username { get; private set; }
    public string Role { get; private set; }

    public AccessTokenDTO(string token, string username, string role)
    {
        Token = token;
        Username = username;
        Role = role;
    }
}