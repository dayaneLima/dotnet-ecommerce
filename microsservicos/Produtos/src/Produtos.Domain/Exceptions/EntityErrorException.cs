using Produtos.Domain.Errors;

namespace Produto.Domain.Exceptions;

public class EntityErrorException: Exception
{
    public EntityError? EntityError { get; }

    public EntityErrorException() { }

    public EntityErrorException(string message)
        : base(message) { }

    public EntityErrorException(string message, Exception inner)
        : base(message, inner) { }

    public EntityErrorException(string message, EntityError entityError)
        : this(message)
    {
        EntityError = entityError;
    }
}