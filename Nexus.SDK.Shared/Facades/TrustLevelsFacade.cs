using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

public class TrustLevelsFacade : ServerFacade
{
    public TrustLevelsFacade(IServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// List Trust Levels and their limits based on query paramaters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Paged list of Partner's trust levels
    /// </returns>
    public async Task<PagedResponse<TrustLevelsResponse>> Get(IDictionary<string, string>? query)
    {
        return await _provider.GetTrustLevels(query);
    }
}
