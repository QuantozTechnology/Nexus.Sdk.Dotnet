using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public interface ITokensFacade
{
    public Task<TokenDetailsResponse> Get(string tokenCode);

    public Task<PagedResponse<TokenResponse>> Get(IDictionary<string, string> query);

    public Task<TokenBalancesResponse> GetTokenBalances(string tokenCode);

    [Obsolete("This endpoint is deprecated and will be removed in future versions. Please use GetTokenFeePayerDetails instead.")]
    public Task<IEnumerable<TokenFeePayerResponse>> GetTokenFeePayerTotals();
    
    public Task<PagedResponse<FeePayerDetailsResponse>> GetTokenFeePayerDetails(IDictionary<string, string> queryParameters);

    public Task<CreateTokenResponse> CreateOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);

    public Task<CreateTokenResponse> CreateOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);

    public Task<CreateTokenResponse> CreateOnStellar(StellarTokenDefinition definition, StellarTokenSettings? settings = null, string? customerIPAddress = null);

    public Task<CreateTokenResponse> CreateOnStellar(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null, string? customerIPAddress = null);
}
