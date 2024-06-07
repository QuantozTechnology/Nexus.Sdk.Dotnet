using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses;

public record CustomerResponse
{
    [JsonConstructor]
    public CustomerResponse(string customerCode,string? name, string firstName, string lastName, string dateOfBirth, string phone, string companyName, string trustLevel, 
        string currencyCode, string countryCode, string? email, string status, string bankAccount, bool isBusiness, string riskQualification, IDictionary<string, string> data)
    {
        CustomerCode = customerCode;
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Phone = phone;
        CompanyName = companyName;
        TrustLevel = trustLevel;
        CurrencyCode = currencyCode;
        CountryCode = countryCode;
        Email = email;
        Status = status;
        BankAccount = bankAccount;
        IsBusiness = isBusiness;
        RiskQualification = riskQualification;
        Data = data;
    }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

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

    [JsonPropertyName("trustlevel")]
    public string TrustLevel { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("bankAccount")]
    public string BankAccount { get; set; }

    [JsonPropertyName("isBusiness")]
    public bool IsBusiness { get; set; }

    [JsonPropertyName("riskQualification")]
    public string? RiskQualification { get; set; }

    [JsonPropertyName("data")]
    public IDictionary<string, string> Data { get; set; }
}

public class DeleteCustomerResponse
{
    [JsonConstructor]
    public DeleteCustomerResponse(string customerCode, string customerStatus, IEnumerable<DeleteAccountResponse> accounts)
    {
        CustomerCode = customerCode;
        CustomerStatus = customerStatus;
        Accounts = accounts;
    }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("customerStatus")]
    public string CustomerStatus { get; set; }

    [JsonPropertyName("accounts")]
    public IEnumerable<DeleteAccountResponse> Accounts { get; set; }
}

public class DeleteAccountResponse
{
    [JsonConstructor]
    public DeleteAccountResponse(string accountCode, string accountStatus)
    {
        AccountCode = accountCode;
        AccountStatus = accountStatus;
    }

    [JsonPropertyName("accountCode")]
    public string AccountCode { get; set; }

    [JsonPropertyName("accountStatus")]
    public string AccountStatus { get; set; }
}