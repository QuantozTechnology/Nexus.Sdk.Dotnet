using Nexus.Sdk.Token.Requests;

namespace Nexus.Sdk.Token.Facades;

public class SubmitFacade : TokenServerFacade, ISubmitFacade
{
    public SubmitFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task OnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests)
    {
        await _provider.SubmitOnStellarAsync(requests);
    }

    public async Task OnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests,
        bool awaitResult = true, CancellationToken cancellationToken = default)
    {
        await _provider.SubmitOnAlgorandAsync(requests, awaitResult, cancellationToken);
    }

    public async Task<bool> WaitForCompletionAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _provider.WaitForCompletionAsync(code, cancellationToken);
    }
}
