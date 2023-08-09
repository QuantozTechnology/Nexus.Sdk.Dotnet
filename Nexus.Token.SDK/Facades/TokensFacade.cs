using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class TokensFacade : TokenServerFacade, ITokensFacade
{
    public TokensFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<TokenDetailsResponse> Get(string tokenCode)
    {
        return await _provider.GetToken(tokenCode);
    }

    public async Task<PagedResponse<TokenResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetTokens(query);
    }

    public async Task<CreateTokenResponse> CreateOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null,
                                                            string? customerIPAddress = null)
    {
        return await _provider.CreateTokenOnAlgorand(definition, settings, customerIPAddress);
    }

    public async Task<CreateTokenResponse> CreateOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null,
                                                            string? customerIPAddress = null)
    {
        return await _provider.CreateTokensOnAlgorand(definitions, settings, customerIPAddress);
    }

    public async Task<CreateTokenResponse> CreateOnStellar(StellarTokenDefinition definition, StellarTokenSettings? settings = null,
                                                           string? customerIPAddress = null)
    {
        return await _provider.CreateTokenOnStellarAsync(definition, settings, customerIPAddress);
    }

    public async Task<CreateTokenResponse> CreateOnStellar(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null,
                                                           string? customerIPAddress = null)
    {
        return await _provider.CreateTokensOnStellarAsync(definitions, settings, customerIPAddress);
    }
}
