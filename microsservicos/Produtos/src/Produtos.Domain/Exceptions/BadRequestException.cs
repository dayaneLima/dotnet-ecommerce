namespace Produtos.Domain.Exceptions;

public class BadRequestException: Exception
{
    public int StatusCode { get; }

    public BadRequestException() { }

    public BadRequestException(string message)
        : base(message) { }

    public BadRequestException(string message, Exception inner)
        : base(message, inner) { }
}