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

        public static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection, Action<TokenServerProviderOptionsBuilder> action)
        {
            var builder = new TokenServerProviderOptionsBuilder();
            action(builder);

            serviceCollection.AddSingleton(builder.GetOptions());
            serviceCollection.AddHttpClient<ITokenServerProvider, TokenServerProvider>();
            serviceCollection.AddSingleton<ITokenServer, TokenServer>();
            return serviceCollection;
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
