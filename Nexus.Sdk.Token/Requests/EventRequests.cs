using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record CreateEventRequest
{
    [JsonPropertyName("customEventTypeCode")]
    [Required, StringLength(100)]
    public required string CustomEventTypeCode { get; set; }

    [JsonPropertyName("customerCode")]
    [StringLength(40)]
    public string? CustomerCode { get; set; }

    [JsonPropertyName("body")]
    [StringLength(4000)]
    public string? body { get; set; }
}