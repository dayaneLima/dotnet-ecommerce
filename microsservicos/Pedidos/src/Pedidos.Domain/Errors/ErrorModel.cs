using System.Text.Json.Serialization;

namespace Pedidos.Domain.Errors;

public record ErrorModel(
    [property: JsonPropertyName("propertyName")] string? PropertyName, 
    [property: JsonPropertyName("description")] string? Description
);