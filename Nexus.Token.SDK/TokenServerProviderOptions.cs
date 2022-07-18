namespace Nexus.Token.SDK
{
    public class TokenServerProviderOptions
    {
        public Uri? ServerUri { get; set; }
        public IDictionary<PaymentMethodType, string> PaymentMethods { get; init; }

        public TokenServerProviderOptions()
        {
            PaymentMethods = new Dictionary<PaymentMethodType, string>();
        }

        public void AddPaymentMethod(PaymentMethodType key, string code)
        {
            PaymentMethods.Add(key, code);
        }
    }
}
