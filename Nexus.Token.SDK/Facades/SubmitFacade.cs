using Nexus.Token.SDK.Requests;

namespace Nexus.Token.SDK.Facades;

public class SubmitFacade : TokenServerFacade, ISubmitFacade
{
    public SubmitFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task OnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests)
    {
        await _provider.SubmitOnStellarAsync(requests);
    }

    public async Task OnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests)
    {
        await _provider.SubmitOnAlgorandAsync(requests);
    }
}
