namespace Pedidos.Domain.Exceptions;

public class AuthException: Exception
{
    public int StatusCode { get; }

    public AuthException() { }

    public AuthException(string message)
        : base(message) { }

    public AuthException(string message, Exception inner)
        : base(message, inner) { }
}