using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses;

public record DocumentStoreSettingsResponse
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("documentStoreType")]
    public string? DocumentStoreType { get; set; }
    [JsonPropertyName("maxFileSizeInMB")]
    public int MaxFileSizeInMB { get; set; }
    [JsonPropertyName("maxFileCount")]
    public int MaxFileCount { get; set; }
}