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

public class AllowedTokens
{
    [JsonPropertyName("addTokens")]
    [JsonConverter(typeof(AddTokensUnionJsonConverter))]
    public AddTokensUnion? AddTokens { get; set; }

    [JsonPropertyName("removeTokens")]
    public string[]? RemoveTokens { get; set; }

    [JsonPropertyName("enableTokens")]
    public string[]? EnableTokens { get; set; }

    [JsonPropertyName("disableTokens")]
    public string[]? DisableTokens { get; set; }
}

/// <summary>
/// Union type for the <c>addTokens</c> field — holds either plain token code strings
/// or <see cref="TokenCodeWithData"/> objects. Serialized to the same JSON array shape
/// accepted by the Nexus API.
/// </summary>
[JsonConverter(typeof(AddTokensUnionJsonConverter))]
public class AddTokensUnion
{
    public IEnumerable<string>? TokenCodes { get; init; }
    public IEnumerable<TokenCodeWithData>? TokenCodesWithData { get; init; }

    public static AddTokensUnion FromTokenCodes(IEnumerable<string> tokenCodes) => new() { TokenCodes = tokenCodes };
    public static AddTokensUnion FromTokenCodesWithData(IEnumerable<TokenCodeWithData> tokenCodesWithData) => new() { TokenCodesWithData = tokenCodesWithData };
}

public class CreateTokenAccountSettings
{
    [JsonPropertyName("allowedTokens")]
    [JsonConverter(typeof(AddTokensUnionJsonConverter))]
    public AddTokensUnion? AllowedTokens { get; set; }
}

public record TokenCodeWithData
{
    [JsonPropertyName("tokenCode")]
    public required string TokenCode { get; init; }

    [JsonPropertyName("data")]
    public required IDictionary<string, string> Data { get; init; }
}
