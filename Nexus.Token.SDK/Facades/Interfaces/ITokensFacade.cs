using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public interface ITokensFacade
{
    public Task<TokenResponse> Get(string tokenCode);

    public Task<PagedResponse<TokenResponse>> Get(IDictionary<string, string> query);

    public Task<CreateTokenResponse> CreateOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);

    public Task<CreateTokenResponse> CreateOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);


    public Task<CreateTokenResponse> CreateOnStellar(StellarTokenDefinition definition, StellarTokenSettings? settings = null, string? customerIPAddress = null);

    public Task<CreateTokenResponse> CreateOnStellar(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null, string? customerIPAddress = null);
}
