using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses;

public record CustomerResponse
{
    [JsonConstructor]
    public CustomerResponse(string customerCode, string trustLevel, string currencyCode, string? email, string status)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
        Email = email;
        Status = status;
    }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("trustLevel")]
    public string TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}
