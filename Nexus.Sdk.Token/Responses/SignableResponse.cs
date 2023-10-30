using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

public record SignableResponse
{
    [JsonPropertyName("transactionEnvelope")]
    public required BlockchainResponse BlockchainResponse { get; set; }
}

public record SignablePaymentResponse : SignableResponse
{
    [JsonPropertyName("payments")]
    public required IEnumerable<TokenOperationResponse> TokenOperationResponse { get; set; }
}

public record SignablePayoutResponse : SignableResponse
{
    [JsonPropertyName("payout")]
    public required PayoutResponse PayoutOperationResponse { get; set; }
}

public record BlockchainResponse
{
    [JsonPropertyName("hash")]
    public string? TransactionGroupHash { get; }

    [JsonPropertyName("signedTransactionEnvelope")]
    public string? EncodedTransactionGroup { get; }

    [JsonPropertyName("requiredSignatures")]
    public RequiredSignaturesResponse[]? RequiredSignatures { get; }

    [JsonConstructor]
    public BlockchainResponse(string? transactionGroupHash, string? encodedTransactionGroup, RequiredSignaturesResponse[]? requiredSignatures)
    {
        TransactionGroupHash = transactionGroupHash;
        EncodedTransactionGroup = encodedTransactionGroup;
        RequiredSignatures = requiredSignatures;
    }
}

public record RequiredSignaturesResponse
{
    [JsonConstructor]
    public RequiredSignaturesResponse(string hash, string encodedTransaction, string publicKey)
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
