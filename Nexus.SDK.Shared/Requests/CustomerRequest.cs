using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Requests;

public class CustomerRequest
{
    [JsonPropertyName("customerCode")]
    public string? CustomerCode { get; set; }

    [JsonPropertyName("trustLevel")]
    public string? TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string? CurrencyCode { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("countryCode")]
    [StringLength(3, MinimumLength = 2)]
    public string? CountryCode { get; set; }

    [JsonPropertyName("externalCustomerCode")]
    [StringLength(40)]
    public string? ExternalCustomerCode { get; set; }

    [JsonPropertyName("isBusiness")]
    public bool? IsBusiness { get; set; } = false;

    public IDictionary<string, string>? Data { get; set; }

    public List<CustomerBankAccountRequest> BankAccounts { get; set; } = new List<CustomerBankAccountRequest>();
}

public class CreateCustomerRequest : CustomerRequest
{
}

public class UpdateCustomerRequest : CustomerRequest
{
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}

public enum CustomerStatus
{
    ACTIVE = 0,
    UNDERREVIEW = 1,
    NEW = 2,
    BLOCKED = 3
}

public class CustomerBankAccountRequest
{
    [JsonPropertyName("bankAccountNumber")]
    public string? BankAccountNumber { get; set; }

    [JsonPropertyName("bankAccountName")]
    public string? BankAccountName { get; set; }

    public BankRequest? Bank { get; set; }
}

public class BankRequest
{
    [JsonPropertyName("bankBicCode")]
    [StringLength(12)]
    public string? BankBicCode { get; set; }

    [JsonPropertyName("bankIBANCode")]
    [StringLength(10)]
    public string? BankIBANCode { get; set; }

    [JsonPropertyName("bankName")]
    [StringLength(100)]
    public string? BankName { get; set; }

    [JsonPropertyName("bankCity")]
    [StringLength(40)]
    public string? BankCity { get; set; }

    [JsonPropertyName("bankCountryCode")]
    public string? BankCountryCode { get; set; }
}


