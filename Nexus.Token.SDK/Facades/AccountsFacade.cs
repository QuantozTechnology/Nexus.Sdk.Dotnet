﻿using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class AccountsFacade : TokenServerFacade
{
    public AccountsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<AccountResponse> Get(string accountCode)
    {
        return await _provider.GetAccount(accountCode);
    }

    public async Task<AccountBalancesResponse> GetBalances(string accountCode)
    {
        return await _provider.GetAccountBalanceAsync(accountCode);
    }

    public async Task<CreateAccountResponse> CreateOnStellarAsync(string customerCode, string address)
    {
        return await _provider.CreateAccountOnStellarAsync(customerCode, address);
    }

    public async Task<CreateAccountResponse> CreateOnAlgorandAsync(string customerCode, string address)
    {
        return await _provider.CreateAccountOnAlgorandAsync(customerCode, address);
    }

    public async Task<SignableResponse> ConnectToTokenAsync(string accountCode, string tokenCode)
    {
        return await _provider.ConnectAccountToTokenAsync(accountCode, tokenCode);
    }

    public async Task<SignableResponse> ConnectToTokensAsync(string accountCode, string[] tokenCodes)
    {
        return await _provider.ConnectAccountToTokensAsync(accountCode, tokenCodes);
    }
}
