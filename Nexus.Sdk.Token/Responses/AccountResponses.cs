using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

public record AccountResponse
{
    [JsonConstructor]
    public AccountResponse(string customerCode, string accountCode, string cryptoCode, string publicKey, string status, string accountType, string customName, string created, TokenSettingsResponse tokenSettings)
    {
        CustomerCode = customerCode;
        AccountCode = accountCode;
        CryptoCode = cryptoCode;
        PublicKey = publicKey;
        Status = status;
        AccountType = accountType;
        CustomName = customName;
        Created = created;
        TokenSettings = tokenSettings;
    }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; }

    [JsonPropertyName("accountCode")]
    public string AccountCode { get; }

    [JsonPropertyName("dcCode")]
    public string CryptoCode { get; }

    [JsonPropertyName("customerCryptoAddress")]
    public string PublicKey { get; set; }

    [JsonPropertyName("accountStatus")]
    public string Status { get; set; }

    [JsonPropertyName("accountType")]
    public string AccountType { get; set; }

    [JsonPropertyName("customName")]
    public string CustomName { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("tokenSettings")]
    public TokenSettingsResponse TokenSettings { get; set; }
}

public record TokenSettingsResponse
{
    [JsonConstructor]
    public TokenSettingsResponse(AllowedTokensResponse[] allowedTokens)
    {
        AllowedTokens = allowedTokens;
    }

    [JsonPropertyName("allowedTokens")]
    public AllowedTokensResponse[] AllowedTokens { get; set; }
}

public record AllowedTokensResponse
{
    [JsonConstructor]
    public AllowedTokensResponse(string tokenCode, string status)
    {
        TokenCode = tokenCode;
        Status = status;
    }

    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public record UpdateTokenAccountResponse
{
    [JsonPropertyName("signedTransactionEnvelope")]
    public string StellarTransactionEnvelope { get; }

    [JsonPropertyName("requiredSignatures")]
    public RequiredSignaturesResponse[] AlgorandTransactions { get; }

    [JsonConstructor]
    public UpdateTokenAccountResponse(string stellarTransactionEnvelope, RequiredSignaturesResponse[] algorandTransactions)
    {
        StellarTransactionEnvelope = stellarTransactionEnvelope;
        AlgorandTransactions = algorandTransactions;
    }
}

public class AccountBalancesResponse
{
    [JsonPropertyName("balances")]
    public IEnumerable<AccountBalance> Balances { get; }

    public bool IsConnectedToToken(string tokenCode)
    {
        return Balances.Any(balance => balance.TokenCode == tokenCode);
    }

    [JsonConstructor]
    public AccountBalancesResponse(IEnumerable<AccountBalance> balances)
    {
        Balances = balances;
    }
}

public record AccountBalance
{
    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; }

    [JsonConstructor]
    public AccountBalance(string tokenCode, decimal amount, bool enabled)
        => (TokenCode, Amount, Enabled) = (tokenCode, amount, enabled);
}
