namespace Produtos.Domain.Exceptions;

public class NotFoundException: Exception
{
    public int StatusCode { get; }

    public NotFoundException() { }

    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string message, Exception inner)
        : base(message, inner) { }
}