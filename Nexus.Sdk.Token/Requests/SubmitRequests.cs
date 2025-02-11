using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

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

    [JsonPropertyName("backgroundSubmit")]
    public bool BackgroundSubmit { get; set; }

    public AlgorandSubmitSignatureRequest(string transactionHash, string signerPublicKey, string signedTransaction,
        bool backgroundSubmit = false)
    {
        CryptoCode = "ALGO";
        TransactionHash = transactionHash;
        SignerPublicKey = signerPublicKey;
        SignedTransaction = signedTransaction;
        BackgroundSubmit = backgroundSubmit;
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

    public StellarSubmitSignatureRequest(string transactionHash, string signerPublicKey, string signedTransaction,
        string? callbackUrl = null)
    {
        CryptoCode = "XLM";
        TransactionHash = transactionHash;
        SignerPublicKey = signerPublicKey;
        SignedTransaction = signedTransaction;
    }
}