using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses;

public record CustomerResponse
{
    [JsonConstructor]
    public CustomerResponse(string customerCode, string trustLevel, string currencyCode, string? email, string status, string bankAccount, bool isBusiness, IDictionary<string, string> data)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
        Email = email;
        Status = status;
        BankAccount = bankAccount;
        IsBusiness = isBusiness;
        Data = data;
    }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("trustlevel")]
    public string TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("bankAccount")]
    public string BankAccount { get; set; }

    [JsonPropertyName("isBusiness")]
    public bool IsBusiness { get; set; }

    [JsonPropertyName("data")]
    public IDictionary<string, string> Data { get; set; }
}
