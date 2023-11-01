using Nexus.Sdk.Token.Requests;

namespace Nexus.Sdk.Token.Facades;

public interface ISubmitFacade
{
    public Task OnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests);

    public Task OnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests);
}
