using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

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

    public async Task<AccountResponse> CreateOnStellarAsync(string customerCode, string publicKey)
    {
        return await _provider.CreateAccountOnStellarAsync(customerCode, publicKey);
    }

    public async Task<SignableResponse> CreateOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
    {
        return await _provider.CreateAccountOnStellarAsync(customerCode, publicKey, allowedTokens);
    }

    public async Task<AccountResponse> CreateOnAlgorandAsync(string customerCode, string publicKey)
    {
        return await _provider.CreateAccountOnAlgorandAsync(customerCode, publicKey);
    }

    public async Task<SignableResponse> CreateOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
    {
        return await _provider.CreateAccountOnAlgorandAsync(customerCode, publicKey, allowedTokens);
    }

    public async Task<SignableResponse> ConnectToTokenAsync(string accountCode, string tokenCode)
    {
        return await _provider.ConnectAccountToTokenAsync(accountCode, tokenCode);
    }

    public async Task<SignableResponse> ConnectToTokensAsync(string accountCode, IEnumerable<string> tokenCodes)
    {
        return await _provider.ConnectAccountToTokensAsync(accountCode, tokenCodes);
    }
}
