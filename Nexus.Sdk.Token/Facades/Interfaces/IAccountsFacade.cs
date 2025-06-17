using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public interface IAccountsFacade
{
    /// <summary>
    /// Get an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <returns>Nexus account matching the provided code</returns>
    public Task<AccountResponse> Get(string accountCode);

    public Task<PagedResponse<AccountResponse>> Get(IDictionary<string, string> query);

    /// <summary>
    /// Get all token balances of an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <returns>List of token balances for the account matching the provided code</returns>
    public Task<AccountBalancesResponse> GetBalances(string accountCode);

    /// <summary>
    /// Create a virtual account
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <param name="generateReceiveAddress">Generate a receive address for the virtual account to receive from blockchain accounts.</param>
    /// <param name="cryptoCode">Blockchain to connect this account to.</param>
    /// <param name="allowedTokens">A list of token codes the account will be connected to upon creation</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="customName">Optional custom name for account</param>
    /// <returns></returns>
    Task<AccountResponse> CreateVirtualAccount(string customerCode, bool generateReceiveAddress, string cryptoCode, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null);

    /// <summary>
    /// Create a new account on the Stellar blockchain
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <param name="publicKey">The public key the new Stellar account</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="customName">Optional custom name for account</param>
    /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
    /// <returns>The Nexus account that is created</returns>
    public Task<AccountResponse> CreateOnStellarAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

    /// <summary>
    /// Create a new account on the Stellar blockchain and connect it with tokens
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <param name="publicKey">The public key the new Stellar account</param>
    /// <param name="allowedTokens">A list of token codes the account will be connected to upon creation</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="customName">Optional custom name for account</param>
    /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided public key</returns>
    public Task<SignableResponse> CreateOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

    /// <summary>
    /// Create a new account on the Algorand blockchain
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <param name="publicKey">The public key the new Algorand account</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="customName">Optional custom name for account</param>
    /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
    /// <returns>The Nexus account that is created</returns>
    public Task<AccountResponse> CreateOnAlgorandAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

    /// <summary>
    /// Create a new account on the Algorand blockchain and connect it with tokens
    /// </summary>
    /// <param name="customerCode">The code of the customer this account is created for</param>
    /// <param name="publicKey">The public key the new Algorand account</param>
    /// <param name="allowedTokens">A list of token codes the account will be connected to upon creation</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="customName">Optional custom name for account</param>
    /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
    /// <returns>A transaction that needs to be signed using the private key that matches the provided public key</returns>
    public Task<SignableResponse> CreateOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

    /// <summary>
    /// Connect a token to an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCode">Unique Nexus identifier of the token that this account will be connected to</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns>A transaction that needs to be signed using the private key of the provided account</returns>
    public Task<SignableResponse> ConnectToTokenAsync(string accountCode, string tokenCode, string? customerIPAddress = null);

    /// <summary>
    /// Connect tokens to an account
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="tokenCodes">A list of unique Nexus identifiers of tokens that this account will be connected to</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns>A transaction that needs to be signed using the private key of the provided account</returns>
    public Task<SignableResponse> ConnectToTokensAsync(string accountCode, IEnumerable<string> tokenCodes, string? customerIPAddress = null);

    /// <summary>
    /// Updates account properties
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    /// <param name="customerCode">Unique Nexus identifier of the customer</param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <param name="updateRequest"></param>
    /// <returns>A transaction that needs to be signed using the private key of the provided account</returns>
    public Task<SignableResponse> Update(string customerCode, string accountCode, UpdateTokenAccountRequest updateRequest, string? customerIPAddress = null);

    /// <summary>
    /// Deletes an account.
    /// Please note that all token balances needs to be 0 and all tokens disabled.
    /// </summary>
    /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
    public Task<NexusResponse> DeleteAccount(string accountCode);
}
