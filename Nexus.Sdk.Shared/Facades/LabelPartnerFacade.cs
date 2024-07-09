using Nexus.Sdk.Shared.Facades.Interfaces;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public class LabelPartnerFacade: ServerFacade, ILabelPartnerFacade
{
    public LabelPartnerFacade(IServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// Get custom data templates
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>Paged list of Partner's custom data templates</returns>
    public async Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? query)
    {
        return await _provider.GetCustomDataTemplates(query);
    }
}