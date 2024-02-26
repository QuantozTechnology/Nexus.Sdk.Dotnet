using Nexus.Sdk.Shared.Requests;

namespace Nexus.Sdk.Shared.Tests
{
    public class CustomerRequestBuilderTests
    {
        [Test]
        public void CustomerRequestBuilderTests_Build_Default()
        {
            var request = new CreateCustomerRequestBuilder("MOCK_CUSTOMER", "Trusted", "EUR").Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
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
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetStatus(status)
                .SetCountry("NL")
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
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_Email()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
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
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_BankAccount()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
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
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Not.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_BankProperties()
        {
            var bankAccount = new CustomerBankAccountRequest
            {
                BankAccountName = "bank_account_name",
                BankAccountNumber = "bank_account_no",
                Bank = new BankRequest
                {
                    BankName = "ING",
                    BankCountryCode = "NL",
                    BankCity = "Amsterdam"
                }
            };

            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
                .AddBankAccount(bankAccount)
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
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

            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
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
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_CustomProperty()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
                .AddCustomProperty("FirstName", "Bob")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_ExternalCustomerReference()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
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
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.ExternalCustomerCode, Is.EqualTo("MOCK_REFERENCE"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_FirstLastName()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetFirstName("First")
                .SetLastName("Last")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.FirstName, Is.EqualTo("First"));
                Assert.That(request.LastName, Is.EqualTo("Last"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_DateOfBirth()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetDateOfBirth("2000-01-10")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.DateOfBirth, Is.EqualTo("2000-01-10"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_Phone()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetPhone("123456789")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.Phone, Is.EqualTo("123456789"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_RiskQualification()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetRiskQualification("Low")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.RiskQualification, Is.EqualTo("Low"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_CompanyName()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetBusiness(true)
                .SetCompanyName("XYZ")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.CompanyName, Is.EqualTo("XYZ"));
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_IsBusiness()
        {
            var request = new CreateCustomerRequestBuilder("MOCK_CUSTOMER", "Trusted", "EUR")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
                .AddCustomProperty("FirstName", "Bob")
                .SetBusiness(true)
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.CurrencyCode, Is.EqualTo("EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Empty);
                Assert.That(request.Data, Is.Not.Null);
            });
        }
    }
}