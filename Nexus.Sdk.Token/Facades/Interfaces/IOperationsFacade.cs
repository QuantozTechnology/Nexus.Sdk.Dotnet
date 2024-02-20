using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public interface IOperationsFacade
{
    /// <summary>
    /// Get token operation details based on the code
    /// </summary>
    /// <param name="code">Unique Nexus identifier of the operation.</param>
    /// <returns>
    /// Return token operation details
    /// </returns>
    public Task<PagedResponse<TokenOperationResponse>> Get(string code);

    /// <summary>
    /// Lists token operations based on the query parameters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Return a paged list of token payments, fundings, payouts and clawbacks
    /// </returns>
    public Task<PagedResponse<TokenOperationResponse>> Get(IDictionary<string, string> query);

    /// <summary>
    /// Fund an account with a token
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that this account will be funded with</param>
    /// <param name="amount">The amount of tokens this account will be funded with</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <param name="message">This value will be put in the Message field of a funding transaction and will not be stored on the blockchain</param>
    /// <param name="paymentReference">Optional reference to bank payment</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    public Task<FundingResponse> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null);

    /// <summary>
    /// Fund an account with tokens
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="definitions">A list of token codes and their respective amounts this account will be funded with</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional memo that is added to the transaction and will be visible on the blockchain</param>
    /// <param name="message">This value will be put in the Message field of a funding transaction and will not be stored on the blockchain</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    public Task<FundingResponse> CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null, string? message = null, string? customerIPAddress = null);

    /// <summary>
    /// Pay a token from one account to another account
    /// </summary>
    /// <param name="senderPublicKey">PublicKey of the account the token is sent FROM</param>
    /// <param name="receiverPublicKey">PublicKey of the account the token is sent TO</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that is payed</param>
    /// <param name="amount">The amount of tokens that is payed</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <param name="message">This value will be put in the Message field of a funding transaction and will not be stored on the blockchain</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided senderPublicKey</returns>
    public Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null, string? message = null, string? customerIPAddress = null);

    /// <summary>
    /// Pay multiple tokens between different accounts
    /// </summary>
    /// <param name="definitions">A list of payments that will be created</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="message">This value will be put in the Message field of a funding transaction and will not be stored on the blockchain</param>
    /// <returns>A transaction that needs to be signed using the private keys of all matching provided senderPublicKeys</returns>
    public Task<SignablePaymentResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null, string? message = null, string? customerIPAddress = null);

    /// <summary>
    /// Withdraw token from an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that will be withdrawn from this account</param>
    /// <param name="amount">The amount of tokens that will be withdrawn from this account</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="memo">An optional message that is added to the transaction and will be visible on the blockchain</param>
    /// <param name="message">This value will be put in the Message field of a funding transaction and will not be stored on the blockchain</param>
    /// <param name="paymentReference">Optional reference to bank payment</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided account</returns>
    public Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null);

    /// <summary>
    /// Simulate the withdrawal of token from an account.
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that should be withdrawn from this account</param>
    /// <param name="amount">The amount of tokens that should be withdrawn from this account</param>
    /// <param name="pm">An optional payment method that is used to calculate fees</param>
    /// <param name="memo">An optional message that is added to the transaction and would be visible on the blockchain</param>
    /// <param name="paymentReference">Optional reference to bank payment</param>
    /// <returns>A simulated withdrawal that includes fees.</returns>
    public Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null);
}
