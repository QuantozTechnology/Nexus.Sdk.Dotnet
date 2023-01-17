using Nexus.Token.SDK.Requests;

namespace Nexus.Token.SDK.Facades;

public class SubmitFacade : TokenServerFacade, ISubmitFacade
{
    public SubmitFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task OnStellarAsync(StellarSubmitRequest request)
    {
        await _provider.SubmitOnStellarAsync(request);
    }

    public async Task OnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests)
    {
        await _provider.SubmitOnAlgorandAsync(requests);
    }
}
