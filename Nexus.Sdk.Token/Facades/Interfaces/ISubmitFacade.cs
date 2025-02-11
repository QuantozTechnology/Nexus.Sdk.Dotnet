using Nexus.Sdk.Token.Requests;

namespace Nexus.Sdk.Token.Facades;

public interface ISubmitFacade
{
    public Task OnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests);

    public Task OnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests,
        bool awaitResult = true, CancellationToken cancellationToken = default);

    public Task<bool> WaitForCompletionAsync(string code, CancellationToken cancellationToken = default);
}
