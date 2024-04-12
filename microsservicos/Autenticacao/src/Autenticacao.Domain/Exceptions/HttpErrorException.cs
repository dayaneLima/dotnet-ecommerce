using System;

namespace Autenticacao.Domain.Exceptions;

public class HttpErrorException : Exception
{
    public int StatusCode { get; }

    public HttpErrorException() { }

    public HttpErrorException(string message)
        : base(message) { }

    public HttpErrorException(string message, Exception inner)
        : base(message, inner) { }

    public HttpErrorException(string message, int statusCode)
        : this(message)
    {
        StatusCode = statusCode;
    }
}
