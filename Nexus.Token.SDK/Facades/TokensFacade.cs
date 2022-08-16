using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;
using System.Collections.Generic;

namespace Nexus.Token.SDK.Facades;

public class TokensFacade : TokenServerFacade
{
    public TokensFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<TokenResponse> Get(string tokenCode)
    {
        return await _provider.GetToken(tokenCode);
    }

    public async Task<PagedResponse<TokenResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetTokens(query);
    }

    public async Task<CreateTokenResponse> CreateOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null)
    {
        return await _provider.CreateTokenOnAlgorand(definition, settings);
    }

    public async Task<CreateTokenResponse> CreateOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null)
    {
        return await _provider.CreateTokensOnAlgorand(definitions, settings);
    }


    public async Task<CreateTokenResponse> CreateOnStellar(StellarTokenDefinition definition, StellarTokenSettings? settings = null)
    {
        return await _provider.CreateTokenOnStellarAsync(definition, settings);
    }

    public async Task<CreateTokenResponse> CreateOnStellar(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null)
    {
        return await _provider.CreateTokensOnStellarAsync(definitions, settings);
    }
}
