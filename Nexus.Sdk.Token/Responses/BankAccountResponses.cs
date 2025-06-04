using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

public record BankAccountResponse
{
    [JsonConstructor]
    public BankAccountResponse(string number, string name, string customerCode, string currencyCode, bool isPrimary, string created, string createdBy, string updated, string updatedby, BankResponse bank)
    {
        Number = number;
        Name = name;
        CustomerCode = customerCode;
        CurrencyCode = currencyCode;
        IsPrimary = isPrimary;
        Created = created;
        CreatedBy = createdBy;
        Updated = updated;
        Updatedby = updatedby;
        Bank = bank;
    }

    [JsonPropertyName("number")]
    public string Number { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    [JsonPropertyName("updated")]
    public string Updated { get; set; }

    [JsonPropertyName("updatedBy")]
    public string Updatedby { get; set; }

    [JsonPropertyName("bank")]
    public BankResponse Bank { get; set; }
}

public record BankResponse
{

    [JsonConstructor]
    public BankResponse(string id, string name, string country, string bicCode, string city)
    {
        Id = id;
        Name = name;
        Country = country;
        BicCode = bicCode;
        City = city;
    }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("bicCode")]
    public string BicCode { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }
}
