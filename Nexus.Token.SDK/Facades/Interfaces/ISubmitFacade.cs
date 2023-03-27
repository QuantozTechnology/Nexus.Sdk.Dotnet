using Nexus.Token.SDK.Requests;

namespace Nexus.Token.SDK.Facades;

public interface ISubmitFacade
{
    public Task OnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests);

    public Task OnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests);
}
