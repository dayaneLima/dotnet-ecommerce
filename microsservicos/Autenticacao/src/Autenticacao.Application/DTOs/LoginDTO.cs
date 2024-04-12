namespace Autenticacao.Application.DTOs;

public class LoginDTO
{
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public LoginDTO(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}