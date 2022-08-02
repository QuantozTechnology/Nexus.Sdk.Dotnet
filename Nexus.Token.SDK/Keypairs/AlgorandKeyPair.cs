using Algorand;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;
using Nexus.Token.SDK.Security;

namespace Nexus.Token.SDK.KeyPairs;

public class AlgorandKeyPair
{
    private readonly Account _account;

    private AlgorandKeyPair(Account account)
    {
        _account = account;
    }

    public static AlgorandKeyPair Generate()
    {
        var account = new Account();
        return new AlgorandKeyPair(account);
    }

    public static AlgorandKeyPair FromPrivateKey(string privateKey)
    {
        var account = new Account(privateKey);
        return new AlgorandKeyPair(account);
    }

    public static AlgorandKeyPair FromPrivateKey(string encryptedPrivateKey, IDecrypter decrypter)
    {
        var privateKey = decrypter.DecryptString(encryptedPrivateKey);
        var account = new Account(privateKey);
        return new AlgorandKeyPair(account);
    }

    public string GetPublicKey()
    {
        return _account.Address.ToString();
    }

    public string GetPrivateKey()
    {
        return _account.ToMnemonic();
    }

    public string GetPrivateKey(IEncrypter encrypter)
    {
        var privateKey = _account.ToMnemonic();
        return encrypter.EncryptString(privateKey);
    }


    public string GetAccountCode()
    {
        return $"ALGO-{GetPublicKey()}";
    }

    public string Sign(string encodedUnsignedTransaction)
    {
        var transaction = Encoder.DecodeFromMsgPack<Transaction>(Convert.FromBase64String(encodedUnsignedTransaction));
        var signedTransaction = _account.SignTransaction(transaction);
        var encodedSignedTransaction = Convert.ToBase64String(Encoder.EncodeToMsgPack(signedTransaction));

        return encodedSignedTransaction;
    }

    /// <summary>
    /// Sign Algorand transactions using this keypair
    /// </summary>
    /// <param name="response">The response containing the Algorand transactions to sign</param>
    /// <returns>A list of Algorand transactions signed by this keypair</returns>
    /// <exception cref="InvalidOperationException">Thrown when there are no Algorand transactions to sign</exception>
    public IEnumerable<AlgorandSubmitRequest> Sign(SignableResponse response)
    {
        if (response.BlockchainResponse.AlgorandTransactions == null)
        {
            throw new InvalidOperationException("Invalid blockchain response, are you using the correct key pair?");
        }

        var unsignedTransactions = response.BlockchainResponse.AlgorandTransactions.Where(r => r.PublicKey == GetPublicKey());
        var submitRequests = new List<AlgorandSubmitRequest>();

        foreach (var unsignedTransaction in unsignedTransactions)
        {
            var encodedUnsignedTransaction = unsignedTransaction.EncodedTransaction;
            var hash = unsignedTransaction.Hash;

            var encodedSignedTransaction = Sign(encodedUnsignedTransaction);

            var submitRequest = new AlgorandSubmitRequest(hash, GetPublicKey(), encodedSignedTransaction);
            submitRequests.Add(submitRequest);
        }

        return submitRequests;
    }
}
