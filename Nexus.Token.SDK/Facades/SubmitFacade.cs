using Nexus.Token.SDK.Requests;

namespace Nexus.Token.SDK.Facades;

public class SubmitFacade : TokenServerFacade
{
    public SubmitFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task OnStellarAsync(StellarSubmitRequest request)
    {
        await _provider.SubmitOnStellarAsync(request);
    }

    public async Task OnAlgorandAsync(AlgorandSubmitRequest request)
    {
        await _provider.SubmitOnAlgorandAsync(request);
    }
}
