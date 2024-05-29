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
    
    [JsonPropertyName("transactionEnvelope")]
    public required TxEnvelopeResponse TransactionEnvelopeResponse { get; set; }
}

public record BlockchainResponse
{
    [JsonPropertyName("code")]
    public string? Code { get; }

    [JsonPropertyName("hash")]
    public string? TransactionGroupHash { get; }

    [JsonPropertyName("signedTransactionEnvelope")]
    public string? EncodedTransactionGroup { get; }

    [JsonPropertyName("requiredSignatures")]
    public RequiredSignaturesResponse[]? RequiredSignatures { get; }

    [JsonPropertyName("signingNeeded")]
    public bool SigningNeeded { get; set; }

    [JsonPropertyName("transactionEnvelopeStatus")]
    public string? TransactionEnvelopeStatus { get; set; }

    [JsonPropertyName("validUntil")]
    public string? ValidUntil { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("created")]
    public string? Created { get; set; }

    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonConstructor]
    public BlockchainResponse(string? code, string? transactionGroupHash, string? encodedTransactionGroup, RequiredSignaturesResponse[]? requiredSignatures)
    {
        Code = code;
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

    [JsonPropertyName("isSigned")]
    public bool IsSigned { get; set; }
}
