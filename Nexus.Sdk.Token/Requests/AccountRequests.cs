using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record CreateTokenAccountRequest
{
    [JsonPropertyName("accountType")]
    public string? AccountType { get; set; } = "MANAGED";

    [JsonPropertyName("accountStatus")]
    public string? AccountStatus { get; set; } = "ACTIVE";

    [JsonPropertyName("customerCryptoAddress")]
    public string? Address { get; set; }

    [JsonPropertyName("tokenSettings")]
    public CreateTokenAccountSettings? TokenSettings { get; set; }

    [JsonPropertyName("customName")]
    public string? CustomName { get; set; }

    [JsonPropertyName("provider")]
    public Provider? Provider { get; set; }
}

public record CreateAccountRequest : CreateTokenAccountRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; }
}

public record CreateStellarAccountRequest : CreateTokenAccountRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "XLM";
}

public record CreateVirtualAccountRequest : CreateTokenAccountRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; }

    [JsonPropertyName("generateReceiveAddress")]
    public bool GenerateReceiveAddress { get; set; }
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

    [JsonPropertyName("customName")]
    public string? CustomName { get; set; }
}

public class UpdateTokenAccountSettings
{
    [JsonPropertyName("allowedTokens")]
    public AllowedTokens? AllowedTokens { get; set; }
}

public class TokenCodeWithData
{
    [JsonPropertyName("tokenCode")]
    public required string TokenCode { get; set; }

    [JsonPropertyName("data")]
    public IDictionary<string, string>? Data { get; set; }
}

public class AllowedTokens
{
    [JsonPropertyName("addTokens")]
    public IEnumerable<TokenCodeWithData>? AddTokens { get; set; }

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
    public IEnumerable<TokenCodeWithData>? AllowedTokens { get; set; }
}

public class Provider
{
    [JsonPropertyName("type")]
    public string? Type { get; set; } = "Undefined";

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}