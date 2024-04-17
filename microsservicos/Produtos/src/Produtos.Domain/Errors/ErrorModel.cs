using System.Text.Json.Serialization;

namespace Produtos.Domain.Errors;

public record ErrorModel(
    [property: JsonPropertyName("propertyName")] string? PropertyName, 
    [property: JsonPropertyName("propertyName")] string? Description
);