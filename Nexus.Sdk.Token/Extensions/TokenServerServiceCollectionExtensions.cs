using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Shared.Options;
using Nexus.Sdk.Token.Security;

namespace Nexus.Sdk.Token.Extensions
{
    public static class TokenServerServiceCollectionExtensions
    {
        public static IServiceCollection AddTokenServer(this IServiceCollection services, ITokenServerProvider provider)
        {
            services.AddScoped(_ => provider);
            services.AddScoped<ITokenServer, TokenServer>();

            return services;
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection services, NexusOptions options)
        {
            services.AddNexusApi(options);
            return services.AddTokenServer();
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection services, Action<NexusOptionsBuilder> action)
        {
            services.AddNexusApi(action);
            return services.AddTokenServer();
        }

        public static IServiceCollection AddTokenServer(this IServiceCollection services, IConfiguration configuration, string sectionName = "NexusOptions")
        {
            services.AddNexusApi(configuration, sectionName);
            return services.AddTokenServer();
        }

        public static IServiceCollection UseSymmetricEncryption(this IServiceCollection serviceCollection, string symmetricKey)
        {
            var aes = new AesOperation(symmetricKey);

            serviceCollection.AddScoped<IEncrypter>(_ => aes);
            serviceCollection.AddScoped<IDecrypter>(_ => aes);

            return serviceCollection;
        }

        private static IServiceCollection AddTokenServer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenServer, TokenServer>();
            serviceCollection.AddScoped<ITokenServerProvider, TokenServerProvider>();
            return serviceCollection;
        }

    }
}
