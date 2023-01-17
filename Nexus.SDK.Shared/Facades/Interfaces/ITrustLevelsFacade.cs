using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

public interface ITrustLevelsFacade
{
    /// <summary>
    /// List Trust Levels and their limits based on query paramaters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Paged list of Partner's trust levels
    /// </returns>
    public Task<PagedResponse<TrustLevelsResponse>> Get(IDictionary<string, string>? query);
}
