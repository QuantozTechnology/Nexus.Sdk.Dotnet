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

    public AlgorandSubmitRequest Sign(SignableResponse response)
    {
        if (response.BlockchainResponse == null)
        {
            throw new ArgumentNullException("No blockchain response to sign");
        }

        if (response.BlockchainResponse.AlgorandTransactions == null)
        {
            throw new InvalidOperationException("Invalid blockchain response, are you using the correct key pair?");
        }

        var unsignedTransaction = response.BlockchainResponse.AlgorandTransactions.First();
        var encodedUnsignedTransaction = unsignedTransaction.EncodedTransaction;
        var hash = unsignedTransaction.Hash;

        var transaction = Encoder.DecodeFromMsgPack<Transaction>(Convert.FromBase64String(encodedUnsignedTransaction));
        var signedTransaction = _account.SignTransaction(transaction);
        var encodedSignedTransaction = Convert.ToBase64String(Algorand.Encoder.EncodeToMsgPack(signedTransaction));

        return new AlgorandSubmitRequest(hash, GetPublicKey(), encodedSignedTransaction);
    }
}
