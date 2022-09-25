using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Token.SDK.Security;

namespace Nexus.Token.SDK.Extensions
{
    public static class TokenServerServiceCollectionExtensions
    {
        public static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection, ITokenServerProvider provider)
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            serviceCollection.AddSingleton(provider);
            serviceCollection.AddSingleton<ITokenServer, TokenServer>();
            return serviceCollection;
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection, TokenServerProviderOptions options)
        {
            serviceCollection.AddSingleton(options);
            serviceCollection.AddHttpClient<ITokenServerProvider, TokenServerProvider>();
            serviceCollection.AddSingleton<ITokenServer, TokenServer>();
            return serviceCollection;
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection, Action<TokenServerProviderOptionsBuilder> action)
        {
            var builder = new TokenServerProviderOptionsBuilder();
            action(builder);

            return AddTokenServer(serviceCollection, builder.GetOptions());
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection, IConfiguration configuration, string configSectionName = "TokenServerOptions")
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configSectionName is null)
            {
                throw new ArgumentNullException(nameof(configSectionName));
            }

            TokenServerProviderOptions options = new();
            configuration.GetSection(configSectionName).Bind(options);

            return AddTokenServer(serviceCollection, options);
        }

        public static IServiceCollection UseSymmetricEncryption(this IServiceCollection serviceCollection, string symmetricKey)
        {
            var aes = new AesOperation(symmetricKey);
            serviceCollection.AddSingleton<IEncrypter>(aes);
            serviceCollection.AddSingleton<IDecrypter>(aes);
            return serviceCollection;
        }
    }
}
