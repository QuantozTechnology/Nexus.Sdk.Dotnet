using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Shared.Authentication;
using Nexus.Sdk.Token.Extensions;

namespace Nexus.Sdk.Shared.Tests;

public class ApplicationScopeHttpClientTests
{
    [Test]
    public async Task NexusApiClient_UsesAuthProviderFromApplicationScope()
    {
        var services = new ServiceCollection();
        var recordingHandler = new RecordingHandler();

        services.AddNexusApi<TestAuthProvider>("https://api.test");
        services.AddHttpClient("NexusApi")
            .ConfigurePrimaryHttpMessageHandler(() => recordingHandler);

        using var root = services.BuildServiceProvider();
        using var scope = root.CreateScope();
        var expectedProvider = scope.ServiceProvider.GetRequiredService<IAuthProvider>();
        var client = scope.ServiceProvider.GetRequiredKeyedService<HttpClient>("NexusApi");

        using var response = await client.GetAsync("accounts");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(recordingHandler.Request, Is.Not.Null);
        Assert.That(recordingHandler.Request!.Headers.Authorization, Is.EqualTo(
            new AuthenticationHeaderValue("Bearer", ((TestAuthProvider)expectedProvider).Token)));
    }

    private sealed class TestAuthProvider : IAuthProvider
    {
        public string Token { get; } = Guid.NewGuid().ToString();

        public Task<string> GetAccessTokenAsync() => Task.FromResult(Token);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public HttpRequestMessage? Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Request = request;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
