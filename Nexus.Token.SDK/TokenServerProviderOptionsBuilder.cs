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
            _options.AddPaymentMethod(PaymentMethodType.Funding, code);
            return this;
        }

        public TokenServerProviderOptionsBuilder AddDefaultPayoutPaymentMethod(string code)
        {
            _options.AddPaymentMethod(PaymentMethodType.Payout, code);
            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToProduction(string clientId, string clientSecret,
            ILogger<ClientAuthProvider>? logger = null)
        {
            _options.ServerUri = new Uri("https://api.quantoznexus.com");

            var authOptions = new ClientAuthProviderOptions("https://identity.quantoznexus.com", clientId, clientSecret);
            var authProvider = new ClientAuthProvider(authOptions, logger);

            _options.AuthProvider = authProvider;

            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToTest(string clientId, string clientSecret, ILogger<ClientAuthProvider>? logger = null)
        {
            _options.ServerUri = new Uri("https://testapi.quantoznexus.com");

            var authOptions = new ClientAuthProviderOptions("https://testidentity.quantoznexus.com", clientId, clientSecret);
            var authProvider = new ClientAuthProvider(authOptions, logger);

            _options.AuthProvider = authProvider;

            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToCustom(string apiUrl, string identityUrl, string clientId, string clientSecret,
            ILogger<ClientAuthProvider>? logger = null)
        {
            _options.ServerUri = new Uri(apiUrl);

            var authOptions = new ClientAuthProviderOptions(identityUrl, clientId, clientSecret);
            var authProvider = new ClientAuthProvider(authOptions, logger);

            _options.AuthProvider = authProvider;

            return this;
        }

        public TokenServerProviderOptions GetOptions()
        {
            return _options;
        }
    }


}
