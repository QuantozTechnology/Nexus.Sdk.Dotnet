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

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    public CustomerRequest(string customerCode, string trustLevel, string currencyCode, string status)
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
        Status = status;
    }
}

public enum CustomerStatus
{
    ACTIVE = 0,
    UNDERREVIEW = 1,
    NEW = 2,
    BLOCKED = 3
}

public class CustomerRequestBuilder
{
    private readonly CustomerRequest _request;

    public CustomerRequestBuilder(string customerCode, string trustLevel, string currencyCode)
    {
        _request = new CustomerRequest(customerCode, trustLevel, currencyCode, "ACTIVE");
    }

    public CustomerRequestBuilder SetEmail(string email)
    {
        _request.Email = email;
        return this;
    }

    public CustomerRequestBuilder SetStatus(CustomerStatus status)
    {
        _request.Status = status.ToString();
        return this;
    }

    public CustomerRequest Build()
    {
        return _request;
    }
}
