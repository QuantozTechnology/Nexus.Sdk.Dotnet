using Nexus.SDK.Shared.Authentication;

namespace Nexus.Token.SDK
{
    public class TokenServerProviderOptions
    {
        public string? ApiUrl { get; set; }
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
        public AuthProviderOptions AuthProviderOptions { get; set; }

        public TokenServerProviderOptions()
        {
            PaymentMethodOptions = new PaymentMethodOptions();
            AuthProviderOptions = new AuthProviderOptions();
        }
    }

    public class PaymentMethodOptions
    {
        public string? Funding { get; set; }
        public string? Payout { get; set; }
    }
}
