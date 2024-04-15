using System.Text.Json;
using System.Text.Json.Serialization;

using Produtos.Domain.Errors;

namespace Produtos.Application.DTOs;

public class ErrorDTO
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("descriptions")]
    public ICollection<ErrorModel> Descriptions { get; set; }

    public ErrorDTO()
    {
        Descriptions = new List<ErrorModel>();
    }

    public void AddError(string description, string propertyName = "sumary")
    {
        Descriptions.Add(new ErrorModel {Description = description, PropertyName = propertyName});
    }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
