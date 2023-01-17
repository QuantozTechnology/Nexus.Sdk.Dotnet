using Nexus.Token.SDK.Requests;

namespace Nexus.Token.SDK.Facades;

public interface ISubmitFacade
{
    public Task OnStellarAsync(StellarSubmitRequest request);

    public Task OnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests);
}
