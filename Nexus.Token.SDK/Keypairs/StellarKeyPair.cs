using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;
using Nexus.Token.SDK.Security;
using stellar_dotnet_sdk;

namespace Nexus.Token.SDK.KeyPairs;

public class StellarKeyPair
{
    public const string TEST_NETWORK = "Test SDF Network ; September 2015";
    public const string PRODUCTION_NETWORK = "Public Global Stellar Network ; September 2015";

    private readonly KeyPair _keyPair;

    private StellarKeyPair(KeyPair keyPair)
    {
        _keyPair = keyPair;
    }

    public static StellarKeyPair Generate()
    {
        var keyPair = KeyPair.Random();
        return new StellarKeyPair(keyPair);
    }

    public static StellarKeyPair FromPrivateKey(string privateKey)
    {
        var keyPair = KeyPair.FromSecretSeed(privateKey);
        return new StellarKeyPair(keyPair);
    }

    public static StellarKeyPair FromPrivateKey(string encryptedPrivateKey, IDecrypter decrypter)
    {
        var privateKey = decrypter.DecryptString(encryptedPrivateKey);
        var keyPair = KeyPair.FromSecretSeed(privateKey);
        return new StellarKeyPair(keyPair);
    }

    public string GetPublicKey()
    {
        return _keyPair.Address;
    }

    public string GetPrivateKey()
    {
        return _keyPair.SecretSeed;
    }

    public string GetPrivateKey(IEncrypter encrypter)
    {
        var privateKey = _keyPair.SecretSeed;
        return encrypter.EncryptString(privateKey);
    }

    public string GetAccountCode()
    {
        return $"XLM-{GetPublicKey()}";
    }

    public string Sign(string unsignedEnvelope, string networkPassphrase)
    {
        var network = new Network(networkPassphrase);
        var transaction = Transaction.FromEnvelopeXdr(unsignedEnvelope);
        transaction.Sign(_keyPair, network);
        var signedEnvelope = transaction.ToEnvelopeXdrBase64();

        return signedEnvelope;
    }

    /// <summary>
    /// Sign a transaction envelope using this keypair
    /// </summary>
    /// <param name="response">The response containing the transaction envelope to sign</param>
    /// <param name="networkPassphrase">>Unique passphrase used when validating signatures on a given transaction</param>
    /// <returns>A transaction envelope signed by this keypair</returns>
    /// <exception cref="InvalidOperationException">Thrown when there is no Stellar transaction envelope to sign</exception>
    public StellarSubmitRequest Sign(SignableResponse response, string networkPassphrase = PRODUCTION_NETWORK)
    {
        if (string.IsNullOrWhiteSpace(response.BlockchainResponse.EncodedStellarEnvelope) || string.IsNullOrWhiteSpace(response.BlockchainResponse.StellarHash))
        {
            throw new InvalidOperationException("Invalid blockchain response, are you using the correct key pair?");
        }

        var unsignedEnvelope = response.BlockchainResponse.EncodedStellarEnvelope;
        var hash = response.BlockchainResponse.StellarHash;

        var signedEnvelope = Sign(unsignedEnvelope, networkPassphrase);
        return new StellarSubmitRequest(signedEnvelope, hash);
    }

    /// <summary>
    /// Sign a transaction envelope that has already been signed by another keypair
    /// </summary>
    /// <param name="request">The request containing the previously signed transaction envelope</param>
    /// <param name="networkPassphrase">Unique passphrase used when validating signatures on a given transaction</param>
    /// <returns>A transaction envelope signed by this keypair</returns>
    /// <exception cref="InvalidOperationException">Thrown when there is no Stellar transaction envelope to sign</exception>
    public StellarSubmitRequest Sign(StellarSubmitRequest request, string networkPassphrase = PRODUCTION_NETWORK)
    {
        if (string.IsNullOrWhiteSpace(request.SignedTransactionEnvelope) || string.IsNullOrWhiteSpace(request.TransactionHash))
        {
            throw new InvalidOperationException("Invalid blockchain response, are you using the correct key pair?");
        }

        var unsignedEnvelope = request.SignedTransactionEnvelope;
        var hash = request.TransactionHash;

        var signedEnvelope = Sign(unsignedEnvelope, networkPassphrase);
        return new StellarSubmitRequest(signedEnvelope, hash);
    }
}
