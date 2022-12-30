using System.ComponentModel.DataAnnotations;
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

    [JsonPropertyName("countryCode")]
    [StringLength(3, MinimumLength = 2)]
    public string CountryCode { get; set; }

    [JsonPropertyName("externalCustomerCode")]
    [StringLength(40)]
    public string ExternalCustomerCode { get; set; }

    [JsonPropertyName("isBusiness")]
    public bool IsBusiness { get; set; } = false;

    public IDictionary<string, string>? Data { get; set; }

    public CustomerBankAccountRequest[]? BankAccounts { get; set; }
}

public record CreateCustomerRequest : CustomerRequest
{
    public CreateCustomerRequest(string customerCode, string trustLevel, string currencyCode, string status)
        : base()
    {
        CustomerCode = customerCode;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
        Status = status;
    }
}

public record UpdateCustomerRequest : CustomerRequest
{
    public UpdateCustomerRequest(string customerCode)
        : base()
    {
        CustomerCode = customerCode;
    }
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

    public CustomerBankRequest? Bank { get; set; }
}

public class CustomerBankRequest
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

public class CustomerRequestBuilder
{
    private readonly CustomerRequest _request;

    public CustomerRequestBuilder(string customerCode, string trustLevel, string currencyCode)
    {
        _request = new CreateCustomerRequest(customerCode, trustLevel, currencyCode, "ACTIVE");
    }

    public CustomerRequestBuilder(string customerCode)
    {
        _request = new UpdateCustomerRequest(customerCode);
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

    public CustomerRequestBuilder AddBankAccount(CustomerBankAccountRequest[] bankAccounts)
    {
        _request.BankAccounts = bankAccounts;
        return this;
    }

    public CustomerRequestBuilder AddBankProperties(string? bankAccountNumber, string? accountName, string? bicCode, string? ibanCode, string? bankName, string? city, string? countryCode)
    {
        var customerBankAccounts = new CustomerBankAccountRequest[]
        {
            new CustomerBankAccountRequest()
            {
                BankAccountNumber = bankAccountNumber,
                BankAccountName = accountName,
                Bank = new CustomerBankRequest
                {
                    BankName = bankName,
                    BankBicCode = bicCode,
                    BankIBANCode = ibanCode,
                    BankCity = city,
                    BankCountryCode = countryCode
                }
            }
        };

        _request.BankAccounts = customerBankAccounts;
        return this;
    }

    public CustomerRequestBuilder SetCustomData(IDictionary<string, string> data)
    {
        _request.Data = data;
        return this;
    }

    public CustomerRequestBuilder AddCustomProperty(string key, string value)
    {
        var data = new Dictionary<string, string>
        {
            [key] = value
        };

        _request.Data = data;
        return this;
    }

    public CustomerRequestBuilder SetCountry(string countryCode)
    {
        _request.CountryCode = countryCode;
        return this;
    }

    public CustomerRequestBuilder SetExternalReference(string externalReference)
    {
        _request.ExternalCustomerCode = externalReference;
        return this;
    }

    public CustomerRequestBuilder SetBusiness(bool isBusiness)
    {
        _request.IsBusiness = isBusiness;
        return this;
    }

    public CustomerRequest Build()
    {
        return _request;
    }
}
