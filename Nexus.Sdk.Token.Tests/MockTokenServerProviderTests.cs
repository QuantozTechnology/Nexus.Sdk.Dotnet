using Microsoft.Extensions.DependencyInjection;
using Nexus.Sdk.Token.Extensions;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Tests.Helpers;

namespace Nexus.Sdk.Token.Tests
{
    public class MockTokenServerProviderTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
        }

        [Test]
        public async Task MockTokenServerProviderTests_CreateFunding()
        {
            _services = new ServiceCollection();

            var mock = new MockTokenServerProvider();

            _services.AddTokenServer(mock);

            var provider = _services.BuildServiceProvider();

            var tokenServerProvider = provider.GetRequiredService<ITokenServerProvider>();

            var fundingResponses = await tokenServerProvider.CreateFundingAsync("accountCode", new List<FundingDefinition>());

            Assert.Multiple(() =>
            {
                Assert.That(tokenServerProvider, Is.Not.Null);
                Assert.That(fundingResponses, Is.Not.Null);
                Assert.That(fundingResponses.Funding, Is.Not.Null);
                Assert.That(fundingResponses.PaymentMethod, Is.Not.Null);
                Assert.That(fundingResponses.PaymentMethod?.PaymentMethodName, Is.Not.Null.Or.Empty);
                
                var firstFundingResponse = fundingResponses.Funding.FirstOrDefault();
                Assert.That(firstFundingResponse?.TokenCode, Is.Not.Null.Or.Empty);
                Assert.That(firstFundingResponse?.FundingPaymentCode, Is.Not.Null.Or.Empty);
                Assert.That(firstFundingResponse?.RequestedAmount, Is.GreaterThan(0));
                Assert.That(firstFundingResponse?.ExecutedAmount, Is.GreaterThan(0));
            });
        }
    }
}
