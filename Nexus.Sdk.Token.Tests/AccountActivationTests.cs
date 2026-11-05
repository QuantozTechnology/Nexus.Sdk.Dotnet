using Microsoft.Extensions.Logging.Abstractions;
using Nexus.Sdk.Shared.Options;
using System.Net;

namespace Nexus.Sdk.Token.Tests;

public class AccountActivationTests
{
    [Test]
    public async Task ActivateAccount_SendsCustomerScopedGetAndCustomerIpHeader()
    {
        var handler = new RecordingHandler();
        var factory = new TestHttpClientFactory(handler);
        var provider = new TokenServerProvider(
            factory,
            new NexusOptions
            {
                ApiUrl = "https://api.example",
                AuthProviderOptions = new AuthProviderOptions(),
                PaymentMethodOptions = new PaymentMethodOptions()
            },
            NullLogger<TokenServerProvider>.Instance);

        await provider.ActivateAccount("CUSTOMER01", "ACCOUNT01", "203.0.113.10");

        Assert.That(handler.Request, Is.Not.Null);
        Assert.That(handler.Request!.Method, Is.EqualTo(HttpMethod.Get));
        Assert.That(handler.Request.RequestUri, Is.EqualTo(new Uri("https://api.example/customer/CUSTOMER01/accounts/activate/ACCOUNT01")));
        Assert.That(handler.Request.Headers.GetValues("customer_ip_address"), Is.EqualTo(new[] { "203.0.113.10" }));
    }

    private sealed class TestHttpClientFactory(HttpMessageHandler handler) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://api.example")
            };
        }
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public HttpRequestMessage? Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
