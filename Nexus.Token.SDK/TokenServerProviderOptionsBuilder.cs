using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.Token.SDK.Security;

namespace Nexus.Token.SDK
{
    public class TokenServerProviderOptionsBuilder
    {
        private readonly TokenServerProviderOptions _options = new();

        public TokenServerProviderOptionsBuilder()
        {
        }

        public TokenServerProviderOptionsBuilder AddDefaultFundingPaymentMethod(string code)
        {
            _options.PaymentMethodOptions.Funding = code;
            return this;
        }

        public TokenServerProviderOptionsBuilder AddDefaultPayoutPaymentMethod(string code)
        {
            _options.PaymentMethodOptions.Payout = code;
            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToProduction(string clientId, string clientSecret,
            ILogger<AuthProvider>? logger = null)
        {
            _options.ApiUrl = "https://api.quantoznexus.com";
            _options.AuthProviderOptions = new AuthProviderOptions("https://identity.quantoznexus.com", clientId, clientSecret);
            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToTest(string clientId, string clientSecret)
        {
            _options.ApiUrl = "https://testapi.quantoznexus.com";
            _options.AuthProviderOptions = new AuthProviderOptions("https://testidentity.quantoznexus.com", clientId, clientSecret);
            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToCustom(string apiUrl, string identityUrl, string clientId, string clientSecret)
        {
            _options.ApiUrl = apiUrl;
            _options.AuthProviderOptions = new AuthProviderOptions(identityUrl, clientId, clientSecret);
            return this;
        }

        public TokenServerProviderOptions GetOptions()
        {
            return _options;
        }
    }
}
