using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses;

public record SignableResponse
{
    [JsonPropertyName("transactionEnvelope")]
    public required BlockchainResponse BlockchainResponse { get; set; }
}

public record SignablePaymentResponse : SignableResponse
{
    [JsonPropertyName("payments")]
    public required TokenOperationResponse? TokenOperationResponse { get; set; }
}

public record SignablePayoutResponse : SignableResponse
{
    [JsonPropertyName("payout")]
    public required PayoutResponse PayoutOperationResponse { get; set; }
}

public record BlockchainResponse
{
    [JsonPropertyName("hash")]
    public string? StellarHash { get; }

    [JsonPropertyName("signedTransactionEnvelope")]
    public string? EncodedStellarEnvelope { get; }

    [JsonPropertyName("requiredSignatures")]
    public AlgorandTransactionResponse[]? AlgorandTransactions { get; }

    [JsonConstructor]
    public BlockchainResponse(string? stellarHash, string? encodedStellarEnvelope, AlgorandTransactionResponse[]? algorandTransactions)
    {
        StellarHash = stellarHash;
        EncodedStellarEnvelope = encodedStellarEnvelope;
        AlgorandTransactions = algorandTransactions;
    }
}

public record AlgorandTransactionResponse
{
    [JsonConstructor]
    public AlgorandTransactionResponse(string hash, string encodedTransaction, string publicKey)
    {
        Hash = hash;
        EncodedTransaction = encodedTransaction;
        PublicKey = publicKey;
    }

    [JsonPropertyName("hash")]
    public string Hash { get; }

    [JsonPropertyName("address")]
    public string PublicKey { get; }

    [JsonPropertyName("transaction")]
    public string EncodedTransaction { get; }
}
