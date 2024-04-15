using System.Text.Json.Serialization;

namespace Produtos.Domain.Errors;

public class ErrorModel
{
    [JsonPropertyName("propertyName")]
    public string? PropertyName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}