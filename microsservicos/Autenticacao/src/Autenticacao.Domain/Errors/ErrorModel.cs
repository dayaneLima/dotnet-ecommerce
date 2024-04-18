using System.Text.Json.Serialization;

namespace Autenticacao.Domain.Errors;

public record ErrorModel(
    [property: JsonPropertyName("propertyName")] string? PropertyName, 
    [property: JsonPropertyName("description")] string? Description
);