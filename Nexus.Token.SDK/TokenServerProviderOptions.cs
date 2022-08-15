using Nexus.SDK.Shared.Authentication;

namespace Nexus.Token.SDK
{
    public class TokenServerProviderOptions
    {
        public Uri? ServerUri { get; set; }
        public IDictionary<PaymentMethodType, string> PaymentMethods { get; } = new Dictionary<PaymentMethodType, string>();
        public IAuthProvider AuthProvider { get; set; }

        public void AddPaymentMethod(PaymentMethodType key, string code)
        {
            PaymentMethods.Add(key, code);
        }
    }
}
