using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public class EventsFacade : TokenServerFacade, IEventsFacade
{
    public EventsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<PagedResponse<EventResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetEvents(query);
    }

    public async Task<EventResponse> Create(CreateEventRequest request, string? customerIPAddress = null)
    {
        return await _provider.CreateEvent(request, customerIPAddress);
    }
}
