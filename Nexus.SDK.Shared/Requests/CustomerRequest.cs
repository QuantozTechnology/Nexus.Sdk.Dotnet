using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Requests;

public record CustomerRequest
{
    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("trustLevel")]
    public string TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    public CustomerRequest(string customerCode, string trustLevel, string currencyCode)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
    }
}
