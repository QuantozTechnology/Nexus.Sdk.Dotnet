using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Token.API;

namespace Nexus.Sdk.Token.Extensions;

public static class StartupExtensions
{
    /*
    public static void AddNexusTokenSdk(this IServiceCollection services, Action<NexusApiOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddHttpClient();
        services.AddScoped<INexusApiClientFactory, NexusApiClientFactory>();
        services.AddScoped<NexusAPIService>();
    }

    public static void AddNexusTokenSdk(this IServiceCollection services)
    {
        services.AddNexusTokenSdk(_ => { });
    }*/
}