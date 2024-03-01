using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Requests;

public class CustomerRequest
{
    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("companyName")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("countryCode")]
    [StringLength(3, MinimumLength = 2)]
    public string? CountryCode { get; set; }

    [JsonPropertyName("riskQualification")]
    public string? RiskQualification { get; set; }

    public IDictionary<string, string>? Data { get; set; }
}

public class CreateCustomerRequest : CustomerRequest
{
    public CreateCustomerRequest() { }

    [JsonPropertyName("trustLevel")]
    public string TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("externalCustomerCode")]
    [StringLength(40)]
    public string? ExternalCustomerCode { get; set; }

    [JsonPropertyName("isBusiness")]
    public bool IsBusiness { get; set; } = false;

    [JsonPropertyName("bankAccounts")]
    public CustomerBankAccountRequest[]? BankAccounts { get; set; }
}

public class UpdateCustomerRequest : CustomerRequest
{
    public UpdateCustomerRequest(){}

    [JsonPropertyName("trustLevelCode")]
    public string? TrustLevel { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("bankAccounts")]
    public UpdateCustomerBankAccountRequest[]? BankAccounts { get; set; }
}

public class CustomerBankAccountRequest
{
    [JsonPropertyName("bankAccountNumber")]
    public string? BankAccountNumber { get; set; }

    [JsonPropertyName("bankAccountName")]
    public string? BankAccountName { get; set; }

    [JsonPropertyName("bank")]
    public BankRequest? Bank { get; set; }
}

public class UpdateCustomerBankAccountRequest
{
    [JsonPropertyName("bankAccountNumber")]
    public string? BankAccountNumber { get; set; }

    [JsonPropertyName("bankAccountName")]
    public string? BankAccountName { get; set; }

    [JsonPropertyName("bank")]
    public BankRequest? Bank { get; set; }

    [JsonPropertyName("status")]
    public UpdateCustomerBankAccountRequestStatus UpdateBankAccountStatus { get; set; }
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

public enum CustomerStatus
{
    ACTIVE = 0,
    UNDERREVIEW = 1,
    NEW = 2,
    BLOCKED = 3,
    DELETED = 4
}

public enum UpdateCustomerBankAccountRequestStatus
{
    Upsert,
    Delete
}
