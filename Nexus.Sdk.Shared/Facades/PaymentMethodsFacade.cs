using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public class PaymentMethodsFacade : ServerFacade, IPaymentMethodsFacade
{
    public PaymentMethodsFacade(IServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// Get payment method data
    /// </summary>
    /// <param name="paymentMethodCode">Unique identifier of the payment method.</param>
    public async Task<PaymentMethodsResponse> Get(string code)
    {
        return await _provider.GetPaymentMethod(code);
    }
}
