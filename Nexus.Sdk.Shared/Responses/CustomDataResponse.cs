using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses;

public record CustomDataResponse
{
    [JsonConstructor]
    public CustomDataResponse(string code, string name, string description, string type, string entityType)
    {
        Code = code;
        Name = name;
        Description = description;
        Type = type;
        EntityType = entityType;
    }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("entityType")]
    public string EntityType { get; set; }
    
}