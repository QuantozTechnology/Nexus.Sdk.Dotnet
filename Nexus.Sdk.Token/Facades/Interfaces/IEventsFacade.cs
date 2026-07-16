using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public interface IEventsFacade
{
    /// <summary>
    /// List events based on the query parameters
    /// </summary>
    /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Return a paged list of events
    /// </returns>
    public Task<PagedResponse<EventResponse>> Get(IDictionary<string, string> query);

    /// <summary>
    /// Create event
    /// </summary>
    /// <param name="request"></param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns></returns>
    public Task<EventResponse> Create(CreateEventRequest request, string? customerIPAddress = null);
}
