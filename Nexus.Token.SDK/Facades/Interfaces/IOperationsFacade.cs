using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public interface IOperationsFacade
{
    /// <summary>
    /// Fund an account with a token
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that this account will be funded with</param>
    /// <param name="amount">The amount of tokens this account will be funded with</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    public Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null);

    /// <summary>
    /// Fund an account with tokens
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="definitions">A list of token codes and their respective amounts this account will be funded with</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    public Task CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null);

    /// <summary>
    /// Pay a token from one account to another account
    /// </summary>
    /// <param name="senderPublicKey">PublicKey of the account the token is sent FROM</param>
    /// <param name="receiverPublicKey">PublicKey of the account the token is sent TO</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that is payed</param>
    /// <param name="amount">The amount of tokens that is payed</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided senderPublicKey</returns>
    public Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null);

    /// <summary>
    /// Pay multiple tokens between different accounts
    /// </summary>
    /// <param name="definitions">A list of payments that will be created</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <returns>A transaction that needs to be signed using the private keys of all matching provided senderPublicKeys</returns>
    public Task<SignableResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null);

    /// <summary>
    /// Withdraw token from an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that will be withdrawn from this account</param>
    /// <param name="amount">The amount of tokens that will be withdrawn from this account</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided account</returns>
    public Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null);
}
