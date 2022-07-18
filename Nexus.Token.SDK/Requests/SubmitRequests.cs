using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Requests;

public record StellarSubmitRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; }

    [JsonPropertyName("envelope")]
    public string SignedTransactionEnvelope { get; set; }

    [JsonPropertyName("hash")]
    public string TransactionHash { get; set; }

public StellarSubmitRequest(string signedTransactionEnvelope, string transactionHash)
    {
        SignedTransactionEnvelope = signedTransactionEnvelope;
        TransactionHash = transactionHash;
        CryptoCode = "XLM";
    }
}

public record AlgorandSubmitRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "ALGO";

    [JsonPropertyName("transactionHash")]
    public string TransactionHash { get; set; }

    [JsonPropertyName("signer")]
    public string SignerPublicKey { get; set; }

    [JsonPropertyName("signedTransaction")]
    public string SignedTransaction { get; set; }

    public AlgorandSubmitRequest(string transactionHash, string signerPublicKey, string signedTransaction)
    {
        TransactionHash = transactionHash;
        SignerPublicKey = signerPublicKey;
        SignedTransaction = signedTransaction;
        CryptoCode = "ALGO";
    }
}
