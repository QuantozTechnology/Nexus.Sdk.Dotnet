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
    /// <param name="callbackUrl">The optional callbackUrl to be used for background submitting notifications</param>
    /// <returns>A list of Stellar transactions signed by this keypair</returns>
    /// <exception cref="InvalidOperationException">Thrown when there is no Stellar transaction envelope to sign</exception>
    public IEnumerable<StellarSubmitSignatureRequest> Sign(SignableResponse response,
        string networkPassphrase = PRODUCTION_NETWORK, string? callbackUrl = null)
    {
        if (response.BlockchainResponse.RequiredSignatures == null)
        {
            throw new InvalidOperationException("Invalid blockchain response, are you using the correct key pair?");
        }

        var unsignedTransactions = response.BlockchainResponse.RequiredSignatures
            .Where(r => r.PublicKey == GetPublicKey());

        // Bit overkill, realistically you'll only ever have one envelope to sign. That said, it doesn't make this wrong
        var submitRequests = unsignedTransactions.Select(unsignedTransaction =>
        {
            var encodedUnsignedTransaction = unsignedTransaction.EncodedTransaction;
            var hash = unsignedTransaction.Hash;

            var encodedSignedTransaction = Sign(encodedUnsignedTransaction, networkPassphrase);

            return new StellarSubmitSignatureRequest(hash, GetPublicKey(), encodedSignedTransaction, callbackUrl);
        });

        return submitRequests;
    }
}
