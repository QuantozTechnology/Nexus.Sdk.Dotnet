using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record CreateTokenAccountRequest
{
    [JsonPropertyName("accountType")]
    public string? AccountType { get; set; } = "MANAGED";

    [JsonPropertyName("customerCryptoAddress")]
    public string? Address { get; set; }

    [JsonPropertyName("tokenSettings")]
    public CreateTokenAccountSettings? TokenSettings { get; set; }

    [JsonPropertyName("customName")]
    public string? CustomName { get; set; }
}

public record CreateStellarAccountRequest : CreateTokenAccountRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "XLM";
}

public record CreateAlgorandAccountRequest : CreateTokenAccountRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "ALGO";
}

public record UpdateTokenAccountRequest
{
    [JsonPropertyName("tokenSettings")]
    public UpdateTokenAccountSettings? Settings { get; set; }
}

public class UpdateTokenAccountSettings
{
    [JsonPropertyName("allowedTokens")]
    public AllowedTokens? AllowedTokens { get; set; }
}

public class AllowedTokens
{
    [JsonPropertyName("addTokens")]
    public IEnumerable<string>? AddTokens { get; set; }

    [JsonPropertyName("removeTokens")]
    public string[]? RemoveTokens { get; set; }

    [JsonPropertyName("enableTokens")]
    public string[]? EnableTokens { get; set; }

    [JsonPropertyName("disableTokens")]
    public string[]? DisableTokens { get; set; }
}

public class CreateTokenAccountSettings
{
    [JsonPropertyName("allowedTokens")]
    public IEnumerable<string>? AllowedTokens { get; set; }
}
