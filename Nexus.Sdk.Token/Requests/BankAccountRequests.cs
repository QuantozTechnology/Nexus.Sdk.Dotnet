using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record CreateBankAccountRequest
{
    [JsonPropertyName("number")]
    [Required, StringLength(40)]
    public required string Number { get; set; }

    [JsonPropertyName("name")]
    [StringLength(80)]
    public string? Name { get; set; }

    [JsonPropertyName("bank")]
    public RelatedBank? Bank { get; set; }

    [JsonPropertyName("customerCode")]
    [Required, StringLength(40)]
    public required string CustomerCode { get; set; }

    [JsonPropertyName("currencyCode")]
    [Required, StringLength(3, MinimumLength = 3)]
    public required string CurrencyCode { get; set; }

    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }
}

public record RelatedBank
{
    [JsonPropertyName("bicCode")]
    public string? BicCode { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("countryCode")]
    [StringLength(2, MinimumLength = 2)]
    public string? CountryCode { get; set; }
}

public record UpdateBankAccountRequest
{
    [JsonPropertyName("number")]
    [Required, StringLength(40)]
    public required string Number { get; set; }

    [JsonPropertyName("name")]
    [StringLength(80)]
    public string? Name { get; set; }

    [JsonPropertyName("bank")]
    public RelatedBank? Bank { get; set; }

    [JsonPropertyName("customerCode")]
    [Required, StringLength(40)]
    public required string CustomerCode { get; set; }

    [JsonPropertyName("currencyCode")]
    [Required, StringLength(3, MinimumLength = 3)]
    public required string CurrencyCode { get; set; }

    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }
}

public record DeleteBankAccountRequest
{
    [JsonPropertyName("number")]
    [Required, StringLength(40)]
    public required string Number { get; set; }

    [JsonPropertyName("customerCode")]
    [Required, StringLength(40)]
    public required string CustomerCode { get; set; }
}