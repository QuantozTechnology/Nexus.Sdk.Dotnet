using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades.Interfaces;

public interface ILabelPartnerFacade
{
    /// <summary>
    /// Get custom data templates
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>Paged list of Partner's custom data templates</returns>
    public Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? query);
}