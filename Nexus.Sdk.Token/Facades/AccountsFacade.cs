using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public class AccountsFacade : TokenServerFacade, IAccountsFacade
{
    public AccountsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<AccountResponse> Get(string accountCode)
    {
        return await _provider.GetAccount(accountCode);
    }

    public async Task<PagedResponse<AccountResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetAccounts(query);
    }

    public async Task<AccountBalancesResponse> GetBalances(string accountCode)
    {
        return await _provider.GetAccountBalanceAsync(accountCode);
    }

    public async Task<SignableResponse> Update(string customerCode, string accountCode, UpdateTokenAccountRequest updateRequest, string? customerIPAddress = null)
    {
        return await _provider.UpdateAccount(customerCode, accountCode, updateRequest, customerIPAddress);
    }

    public async Task<AccountResponse> CreateOnStellarAsync(string customerCode, string publicKey, string? customerIPAddress = null)
    {
        return await _provider.CreateAccountOnStellarAsync(customerCode, publicKey, customerIPAddress);
    }

    public async Task<SignableResponse> CreateOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null)
    {
        return await _provider.CreateAccountOnStellarAsync(customerCode, publicKey, allowedTokens, customerIPAddress);
    }

    public async Task<AccountResponse> CreateOnAlgorandAsync(string customerCode, string publicKey, string? customerIPAddress = null)
    {
        return await _provider.CreateAccountOnAlgorandAsync(customerCode, publicKey, customerIPAddress);
    }

    public async Task<SignableResponse> CreateOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null)
    {
        return await _provider.CreateAccountOnAlgorandAsync(customerCode, publicKey, allowedTokens, customerIPAddress);
    }

    public async Task<SignableResponse> ConnectToTokenAsync(string accountCode, string tokenCode, string? customerIPAddress = null)
    {
        return await _provider.ConnectAccountToTokenAsync(accountCode, tokenCode, customerIPAddress);
    }

    public async Task<SignableResponse> ConnectToTokensAsync(string accountCode, IEnumerable<string> tokenCodes, string? customerIPAddress = null)
    {
        return await _provider.ConnectAccountToTokensAsync(accountCode, tokenCodes, customerIPAddress);
    }
}
