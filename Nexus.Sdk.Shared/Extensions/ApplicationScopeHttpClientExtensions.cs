using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

namespace Nexus.Sdk.Token.Extensions;

public static class ApplicationScopeHttpClientExtensions
{
    public static readonly HttpRequestOptionsKey<IServiceProvider> ScopeKey =
        new("ApplicationScope");

    public static IHttpClientBuilder AddApplicationScopeHandler(
        this IHttpClientBuilder builder)
    {
        var name = builder.Name;

        builder.Services.AddTransient<ApplicationScopeHandler>();
        builder.Services.AddKeyedScoped<HttpClient>(name, (serviceProvider, _) =>
        {
            var handler = serviceProvider.GetRequiredService<ApplicationScopeHandler>();
            handler.InnerHandler = serviceProvider
                .GetRequiredService<IHttpMessageHandlerFactory>()
                .CreateHandler(name);

            var client = new HttpClient(handler, disposeHandler: false);
            var options = serviceProvider
                .GetRequiredService<IOptionsMonitor<HttpClientFactoryOptions>>()
                .Get(name);

            foreach (var action in options.HttpClientActions)
            {
                action(client);
            }

            return client;
        });

        return builder;
    }
}

public sealed class ApplicationScopeHandler(IServiceProvider serviceProvider) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Options.Set(ApplicationScopeHttpClientExtensions.ScopeKey, serviceProvider);
        return base.SendAsync(request, cancellationToken);
    }
}
