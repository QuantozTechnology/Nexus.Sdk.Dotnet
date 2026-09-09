using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

public record EventResponse
{
    [JsonConstructor]
    public EventResponse(string id, string customerCode, string customEventTypeCode, string body, string created, string createdBy)
    {
        Id = id;
        CustomerCode = customerCode;
        CustomEventTypeCode = customEventTypeCode;
        Body = body;
        Created = created;
        CreatedBy = createdBy;
    }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; }

    [JsonPropertyName("customEventTypeCode")]
    public string CustomEventTypeCode { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }
}