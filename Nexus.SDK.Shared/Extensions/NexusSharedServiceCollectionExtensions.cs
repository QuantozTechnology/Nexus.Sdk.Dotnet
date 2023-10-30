using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nexus.Sdk.Shared.Authentication;
using Nexus.Sdk.Shared.Http;
using Nexus.Sdk.Shared.Options;

namespace Nexus.Sdk.Token.Extensions
{
    public static class NexusSharedServiceCollectionExtensions
    {
        public static IServiceCollection AddNexusApi(this IServiceCollection services, NexusOptions options)
        {
            services.AddSingleton(options);
            return services.AddNexusApi();
        }

        public static IServiceCollection AddNexusApi(this IServiceCollection services, Action<NexusOptionsBuilder> action)
        {
            var builder = new NexusOptionsBuilder();
            action(builder);

            var options = builder.GetOptions();
            services.AddSingleton(options);

            return services.AddNexusApi();
        }

        public static IServiceCollection AddNexusApi(this IServiceCollection services, IConfiguration configuration, string sectionName = "NexusOptions")
        {
            services.AddOptions<NexusOptions>()
                .Bind(configuration.GetSection(sectionName))
                .ValidateDataAnnotationsRecursively()
                .ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<NexusOptions>>().Value);

            return services.AddNexusApi();
        }

        private static IServiceCollection AddNexusApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthProvider, AuthProvider>();
            serviceCollection.AddHttpClient<AuthProvider>();
            serviceCollection.AddScoped<NexusIdentityHandler>();
            serviceCollection.AddHttpClient("NexusApi", (provider, client) =>
            {
                var options = provider.GetRequiredService<NexusOptions>();
                client.BaseAddress = new Uri(options.ApiUrl);
                client.DefaultRequestHeaders.Add("api_version", "1.2");
            })
            .AddHttpMessageHandler<NexusIdentityHandler>();

            return serviceCollection;
        }
    }
}
