using Nexus.SDK.Shared.Requests;

namespace Nexus.SDK.Shared.Tests
{
    public class CustomerRequestBuilderTests
    {
        [Test]
        public void CustomerRequestBuilderTests_Build_Default()
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR").Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.Null);
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(CustomerStatus.UNDERREVIEW, "UNDERREVIEW")]
        [TestCase(CustomerStatus.BLOCKED, "BLOCKED")]
        [TestCase(CustomerStatus.NEW, "NEW")]
        [TestCase(CustomerStatus.ACTIVE, "ACTIVE")]
        public void CustomerRequestBuilderTests_Build_Status_Status(CustomerStatus status, string expected)
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetStatus(status)
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.Null);
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo(expected));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_Email()
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_BankAccount()
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetBankAccounts(new CustomerBankAccountRequest[]
                {
                        new CustomerBankAccountRequest()
                        {
                            Bank = null,
                            BankAccountName = "Test_Name"
                        }
                })
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Not.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_CustomData()
        {
            var dataDict = new Dictionary<string, string>(2);
            dataDict.Add("FirstName", "Bob");
            dataDict.Add("LastName", "Saget");

            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCustomData(dataDict)
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_CustomProperty()
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCustomProperty("FirstName", "Bob")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_ExternalCustomerReference()
        {
            var request = new CustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetExternalReference("MOCK_REFERENCE")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo("NL"));
                Assert.That(request.ExternalCustomerCode, Is.EqualTo("MOCK_REFERENCE"));
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }
    }
}