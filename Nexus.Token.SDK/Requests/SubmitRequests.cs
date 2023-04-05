using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Requests;

public record AlgorandSubmitSignatureRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "ALGO";

    [JsonPropertyName("transactionHash")]
    public string TransactionHash { get; set; }

    [JsonPropertyName("signer")]
    public string SignerPublicKey { get; set; }

    [JsonPropertyName("signedTransaction")]
    public string SignedTransaction { get; set; }

    [JsonPropertyName("callbackUrl")]
    public string? CallBackUrl { get; set; }

    public AlgorandSubmitSignatureRequest(string transactionHash, string signerPublicKey, string signedTransaction,
        string? callbackUrl = null)
    {
        CryptoCode = "ALGO";
        TransactionHash = transactionHash;
        SignerPublicKey = signerPublicKey;
        SignedTransaction = signedTransaction;
        CallBackUrl = callbackUrl;
    }
}

public record StellarSubmitSignatureRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "XLM";

    [JsonPropertyName("transactionHash")]
    public string TransactionHash { get; set; }

    [JsonPropertyName("signer")]
    public string SignerPublicKey { get; set; }

    [JsonPropertyName("signedTransaction")]
    public string SignedTransaction { get; set; }

    [JsonPropertyName("callbackUrl")]
    public string? CallBackUrl { get; set; }

    public StellarSubmitSignatureRequest(string transactionHash, string signerPublicKey, string signedTransaction,
        string? callbackUrl = null)
    {
        CryptoCode = "XLM";
        TransactionHash = transactionHash;
        SignerPublicKey = signerPublicKey;
        SignedTransaction = signedTransaction;
        CallBackUrl = callbackUrl;
    }
}