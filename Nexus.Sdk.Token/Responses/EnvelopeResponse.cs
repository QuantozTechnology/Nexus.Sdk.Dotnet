using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

public class EnvelopeResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("hash")]
    public string Hash { get; set; }
    [JsonPropertyName("envelope")]
    public string Envelope { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("created")]
    public string Created { get; set; }
    [JsonPropertyName("validUntil")]
    public string ValidUntil { get; set; }
    [JsonPropertyName("memo")]
    public string Memo { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
