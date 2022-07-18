using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses;

public record CustomerResponse
{
    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; }

    [JsonPropertyName("trustLevel")]
    public string TrustLevel { get; }

    [JsonPropertyName("status")]
    public string Status { get; }

    [JsonConstructor]
    public CustomerResponse(string customerCode, string trustLevel, string status)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        Status = status;
    }
}

public record CreateCustomerResponse
{
    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; }

    [JsonPropertyName("trustLevel")]
    public string TrustLevel { get; }

    [JsonPropertyName("status")]
    public string Status { get; }

    [JsonConstructor]
    public CreateCustomerResponse(string customerCode, string trustLevel, string status)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        Status = status;
    }

}
