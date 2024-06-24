using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public interface IPaymentMethodsFacade
{
    public Task<PaymentMethodsResponse> Get(string code);
}
