using Microsoft.Extensions.DependencyInjection;
using Nexus.SDK.Shared.Authentication;
using Nexus.Token.SDK.Extensions;
using Nexus.Token.SDK.Tests.Helpers;

namespace Nexus.Token.SDK.Tests
{
    public class ServiceCollectionTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public void ServiceCollectionTests_Create_TokenServer()
        {
            _services = new ServiceCollection();
            _services.AddTokenServer(o => o.ConnectToProduction("test_client_id", "test_client_secret"));

            var provider = _services.BuildServiceProvider();

            // Throws errors if the services does not exist.
            var tokenServerProvider = provider.GetRequiredService<ITokenServerProvider>();
            var tokenServer = provider.GetRequiredService<ITokenServer>();

            Assert.Multiple(() =>
            {
                Assert.That(tokenServerProvider, Is.Not.Null);
                Assert.That(tokenServer, Is.Not.Null);
            });
        }

        [Test]
        public void ServiceCollectionTests_Create_Production_Options()
        {
            _services = new ServiceCollection();
            _services.AddTokenServer(o => o.ConnectToProduction("test_client_id", "test_client_secret"));

            var provider = _services.BuildServiceProvider();
            var serverOptions = provider.GetRequiredService<TokenServerProviderOptions>();

            Assert.Multiple(() =>
            {
                Assert.That(serverOptions.ServerUri?.ToString(), Is.EqualTo("https://api.quantoznexus.com/"));
                Assert.That(serverOptions.PaymentMethods, Is.Empty);
                Assert.That(serverOptions.AuthProvider, Is.Not.Null);
            });
        }

        [Test]
        public void ServiceCollectionTests_Create_Test_Options()
        {
            _services = new ServiceCollection();
            _services.AddTokenServer(o => o.ConnectToTest("test_client_id", "test_client_secret"));

            var provider = _services.BuildServiceProvider();
            var serverOptions = provider.GetRequiredService<TokenServerProviderOptions>();

            Assert.Multiple(() =>
            {
                Assert.That(serverOptions.ServerUri?.ToString(), Is.EqualTo("https://testapi.quantoznexus.com/"));
                Assert.That(serverOptions.PaymentMethods, Is.Empty);
                Assert.That(serverOptions.AuthProvider, Is.Not.Null);
            });
        }

        [Test]
        public void ServiceCollectionTests_Create_Custom_Options()
        {
            _services = new ServiceCollection();
            _services.AddTokenServer(o =>
                o.ConnectToCustom("https://testapi.com", "https://testidentity.com", "test_client_id", "test_client_secret"));

            var provider = _services.BuildServiceProvider();
            var serverOptions = provider.GetRequiredService<TokenServerProviderOptions>();

            Assert.Multiple(() =>
            {
                Assert.That(serverOptions.ServerUri?.ToString(), Is.EqualTo("https://testapi.com/"));
                Assert.That(serverOptions.PaymentMethods, Is.Empty);
                Assert.That(serverOptions.AuthProvider, Is.Not.Null);
            });
        }

        [Test]
        public void ServiceCollectionTests_Create_Options_With_PaymentMethods()
        {
            _services = new ServiceCollection();
            _services.AddTokenServer(o =>
                o.AddDefaultPayoutPaymentMethod("test_payout_paymentmethod")
                .AddDefaultFundingPaymentMethod("test_funding_paymentmethod"));

            var provider = _services.BuildServiceProvider();
            var serverOptions = provider.GetRequiredService<TokenServerProviderOptions>();

            Assert.Multiple(() =>
            {
                Assert.That(serverOptions.PaymentMethods, Is.Not.Empty);
                Assert.That(serverOptions.PaymentMethods[PaymentMethodType.Funding], Is.EqualTo("test_funding_paymentmethod"));
                Assert.That(serverOptions.PaymentMethods[PaymentMethodType.Payout], Is.EqualTo("test_payout_paymentmethod"));
            });
        }

        [Test]
        public async Task ServiceCollectionTests_Create_TokenServer_With_Custom_Provider()
        {
            _services = new ServiceCollection();

            var mock = new MockTokenServerProvider();

            _services.AddTokenServer(mock);

            var provider = _services.BuildServiceProvider();

            // Assert that the services exist.
            var tokenServerProvider = provider.GetRequiredService<ITokenServerProvider>();
            var response = await tokenServerProvider.CreateCustomer("MOCK_CUSTOMER", "Trusted", "Active");

            Assert.Multiple(() =>
            {
                Assert.That(response.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(response.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(response.Status, Is.EqualTo("Active"));
            });

            var tokenServer = provider.GetRequiredService<ITokenServer>();
            response = await tokenServer.Customers.Create("MOCK_CUSTOMER", "Trusted", "Active");

            Assert.Multiple(() =>
            {
                Assert.That(response.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(response.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(response.Status, Is.EqualTo("Active"));
            });
        }
    }
}