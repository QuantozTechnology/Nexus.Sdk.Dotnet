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

                Assert.That(fundingResponses.TransactionEnvelope, Is.Not.Null);
                Assert.That(fundingResponses.TransactionEnvelope?.Code, Is.Not.Null.Or.Empty);
                Assert.That(fundingResponses.TransactionEnvelope?.Hash, Is.Not.Null.Or.Empty);
                Assert.That(fundingResponses.TransactionEnvelope?.SignedTransactionEnvelope, Is.Not.Null.Or.Empty);
                Assert.That(fundingResponses.TransactionEnvelope?.SigningNeeded, Is.False);
                Assert.That(fundingResponses.TransactionEnvelope?.Memo, Is.Null.Or.Empty);
                Assert.That(fundingResponses.TransactionEnvelope?.Status, Is.Not.Null.Or.Empty);
            });
        }


        [Test]
        public async Task MockTokenServerProviderTests_UpdateOperationStatusAsync()
        {
            _services = new ServiceCollection();

            var mock = new MockTokenServerProvider();

            _services.AddTokenServer(mock);

            var provider = _services.BuildServiceProvider();

            var tokenServerProvider = provider.GetRequiredService<ITokenServerProvider>();

            var newStatus = "test status";
            var newPaymentReference = "test payment reference";
            var tokenOperationResponse = await tokenServerProvider.UpdateOperationStatusAsync("operationCode", newStatus, paymentReference: newPaymentReference);

            Assert.Multiple(() =>
            {
                Assert.That(tokenServerProvider, Is.Not.Null);
                Assert.That(tokenOperationResponse, Is.Not.Null);
                Assert.That(tokenOperationResponse.Code, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.Hash, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.SenderAccount, Is.Not.Null);
                Assert.That(tokenOperationResponse.ReceiverAccount, Is.Not.Null);
                Assert.That(tokenOperationResponse.Amount, Is.EqualTo(100));
                Assert.That(tokenOperationResponse.Created, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.Finished, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.Status, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.Status, Is.EqualTo(newStatus));
                Assert.That(tokenOperationResponse.Type, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.Memo, Is.Not.Null);
                Assert.That(tokenOperationResponse.Message, Is.Not.Null);
                Assert.That(tokenOperationResponse.CryptoCode, Is.Not.Null);
                Assert.That(tokenOperationResponse.TokenCode, Is.Not.Null.Or.Empty);
                Assert.That(tokenOperationResponse.PaymentReference, Is.Not.Null);
                Assert.That(tokenOperationResponse.PaymentReference, Is.EqualTo(newPaymentReference));                
                Assert.That(tokenOperationResponse.FiatAmount, Is.EqualTo(100));
                Assert.That(tokenOperationResponse.NetFiatAmount, Is.EqualTo(100));
                Assert.That(tokenOperationResponse.BlockchainTransactionId, Is.Not.Null);
            });
        }
    }
}
