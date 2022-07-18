using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.Token.SDK.Security;

namespace Nexus.Token.SDK
{
    public class TokenServerProviderOptionsBuilder
    {
        private readonly TokenServerProviderOptions _options = new();
        private readonly IServiceCollection serviceCollection;

        public TokenServerProviderOptionsBuilder(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
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

        public TokenServerProviderOptionsBuilder UseSymetricEncryption(string symetricKey)
        {
            var aes = new AesOperation(symetricKey);
            serviceCollection.AddSingleton<IEncrypter>(aes);
            serviceCollection.AddSingleton<IDecrypter>(aes);
            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToProduction(string clientId, string clientSecret)
        {
            _options.ServerUri = new Uri("https://api.quantoznexus.com");

            var authOptions = new ClientAuthProviderOptions("https://identity.quantoznexus.com", clientId, clientSecret);
            serviceCollection.AddSingleton(authOptions);
            serviceCollection.AddSingleton<IAuthProvider, ClientAuthProvider>();

            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToTest(string clientId, string clientSecret)
        {
            _options.ServerUri = new Uri("https://testapi.quantoznexus.com");

            var authOptions = new ClientAuthProviderOptions("https://testidentity.quantoznexus.com", clientId, clientSecret);
            serviceCollection.AddSingleton(authOptions);
            serviceCollection.AddSingleton<IAuthProvider, ClientAuthProvider>();

            return this;
        }

        public TokenServerProviderOptionsBuilder ConnectToCustom(string apiUrl, string identityUrl, string clientId, string clientSecret)
        {
            _options.ServerUri = new Uri(apiUrl);

            var authOptions = new ClientAuthProviderOptions(identityUrl, clientId, clientSecret);
            serviceCollection.AddSingleton(authOptions);
            serviceCollection.AddSingleton<IAuthProvider, ClientAuthProvider>();

            return this;
        }

        public TokenServerProviderOptions GetOptions()
        {
            return _options;
        }
    }


}
