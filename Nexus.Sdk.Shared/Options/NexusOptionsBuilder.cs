namespace Nexus.Sdk.Shared.Options
{
    public class NexusOptionsBuilder
    {
        private readonly NexusOptions _options = new()
        {
            ApiUrl = string.Empty,
            AuthProviderOptions = new AuthProviderOptions
            {
                ClientId = string.Empty,
                ClientSecret = string.Empty,
                IdentityUrl = string.Empty,
                Scopes = string.Empty
            },
            PaymentMethodOptions = new PaymentMethodOptions()
        };

        public NexusOptionsBuilder()
        {
        }

        public NexusOptionsBuilder AddDefaultFundingPaymentMethod(string code)
        {
            _options.PaymentMethodOptions.Funding = code;
            return this;
        }

        public NexusOptionsBuilder AddDefaultPayoutPaymentMethod(string code)
        {
            _options.PaymentMethodOptions.Payout = code;
            return this;
        }

        public NexusOptionsBuilder ConnectToProduction(string clientId, string clientSecret)
        {
            _options.ApiUrl = "https://api.quantoz.com";
            _options.AuthProviderOptions = new AuthProviderOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                IdentityUrl = "https://identity.quantoz.com",
                Scopes = "api1"
            };
            return this;
        }

        public NexusOptionsBuilder ConnectToTest(string clientId, string clientSecret)
        {
            _options.ApiUrl = "https://testapi.quantoz.com";
            _options.AuthProviderOptions = new AuthProviderOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                IdentityUrl = "https://testidentity.quantoz.com",
                Scopes = "api1"
            };

            return this;
        }

        public NexusOptionsBuilder ConnectToCustom(string apiUrl, string identityUrl, string clientId, string clientSecret)
        {
            _options.ApiUrl = apiUrl;
            _options.AuthProviderOptions = new AuthProviderOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                IdentityUrl = identityUrl,
                Scopes = "api1"
            };

            return this;
        }

        public NexusOptions GetOptions()
        {
            return _options;
        }
    }
}
