using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

public class TrustLevelsFacade : ServerFacade
{
    public TrustLevelsFacade(IServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// List Trust Levels and their limits
    /// </summary>
    /// <returns>
    /// Paged list of Partner's trust levels
    /// </returns>
    public async Task<PagedResponse<TrustLevelsResponse>> Get()
    {
        return await _provider.GetTrustLevels();
    }
}
