using Nexus.Sdk.Shared.Requests;

namespace Nexus.Sdk.Shared.Tests
{
    public class CustomerRequestBuilderTests
    {
        [Test]
        public void CustomerRequestBuilderTests_Build_Default()
        {
            var request = new CreateCustomerRequestBuilder("MOCK_CUSTOMER", "Trusted", "EUR")
                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.CurrencyCode, Is.EqualTo(expected: "EUR"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        [TestCase(CustomerStatus.UNDERREVIEW, "UNDERREVIEW")]
        [TestCase(CustomerStatus.BLOCKED, "BLOCKED")]
        [TestCase(CustomerStatus.NEW, "NEW")]
        [TestCase(CustomerStatus.ACTIVE, "ACTIVE")]
        [TestCase(CustomerStatus.DELETED, "DELETED")]
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
                Assert.That(request.BankAccounts, Is.Null);
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
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_BankAccount()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetBankAccounts(new CustomerBankAccountRequest[]
                {
                    new()
                    {
                        IdentifiedBankAccountName = "Test_Name",
                        BankAccountNumber = "NLABN12345",
                        Bank = new BankRequest()
                        {
                            BankIBANCode = "123",
                        }
                    }
                })
                .SetEmail("test@test.com")
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
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.ExternalCustomerCode, Is.Null);
                Assert.That(request.IsBusiness, Is.False);

                Assert.That(request.BankAccounts, Is.Not.Null);
                Assert.That(request.BankAccounts, Has.One.Property("IdentifiedBankAccountName").EqualTo("Test_Name"));
                Assert.That(request.BankAccounts, Has.One.Property("BankAccountNumber").EqualTo("NLABN12345"));
                Assert.That(request.BankAccounts, Has.One.Property("Bank").Property("BankIBANCode").EqualTo("123"));

                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_CustomData()
        {
            var dataDict = new Dictionary<string, string>(2)
            {
                { "FirstName", "Bob" },
                { "LastName", "Saget" }
            };

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
                Assert.That(request.BankAccounts, Is.Null);
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
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_ExternalCustomerReference()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetExternalCustomerCode("MOCK_REFERENCE")
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
                Assert.That(request.CountryCode, Is.EqualTo(expected: "NL"));
                Assert.That(request.ExternalCustomerCode, Is.EqualTo("MOCK_REFERENCE"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_FirstLastName()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetName("First Middle Last")
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
                Assert.That(request.Name, Is.EqualTo("First Middle Last"));
                Assert.That(request.FirstName, Is.EqualTo("First"));
                Assert.That(request.LastName, Is.EqualTo("Last"));
                Assert.That(request.IsBusiness, Is.False);
                Assert.That(request.BankAccounts, Is.Null);
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
                Assert.That(request.BankAccounts, Is.Null);
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
                Assert.That(request.BankAccounts, Is.Null);
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
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_BusinessName()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetIsBusiness(true)
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetName("XYZ")
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
                Assert.That(request.Name, Is.EqualTo("XYZ"));
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_IsReviewRecommended()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetIsBusiness(true)
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetCompanyName("XYZ")
                .SetIsReviewRecommended(true)
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
                Assert.That(request.IsReviewRecommended, Is.True);
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_Address_State_ZipCode_City()
        {
            var request = new CreateCustomerRequestBuilder(
                "MOCK_CUSTOMER", "Trusted", "EUR")
                .SetIsBusiness(true)
                .SetEmail("test@test.com")
                .SetCountry("NL")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetAddress("Netherlands")
                .SetCity("Amsterdam")
                .SetZipCode("1234TR")
                .SetState("State")
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
                Assert.That(request.Address, Is.EqualTo("Netherlands"));
                Assert.That(request.ZipCode, Is.EqualTo("1234TR"));
                Assert.That(request.City, Is.EqualTo("Amsterdam"));
                Assert.That(request.State, Is.EqualTo("State"));
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_IsBusiness()
        {
            var request = new CreateCustomerRequestBuilder("MOCK_CUSTOMER", "Trusted", "EUR")
                .SetIsBusiness(true)
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
                Assert.That(request.IsBusiness, Is.True);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_UpdateCustomerRequest()
        {
            var request = new UpdateCustomerRequestBuilder("MOCK_CUSTOMER")
                .SetTrustLevel("Trusted")
                .SetReason("Reason")
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
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.BankAccounts, Is.Null);
                Assert.That(request.Data, Is.Not.Null);
            });
        }

        [Test]
        public void CustomerRequestBuilderTests_Build_UpdateCustomerRequest_WithBankAccount()
        {
            var request = new UpdateCustomerRequestBuilder("MOCK_CUSTOMER")
                .SetTrustLevel("Trusted")
                .SetBankAccounts(new UpdateCustomerBankAccountRequest[]
                    {
                        new()
                        {
                            IdentifiedBankAccountName = "Test_Name",
                            BankAccountNumber = "NLABN12345",
                            Bank = new BankRequest()
                            {
                                BankIBANCode = "123",
                            }
                        }
                })
                .SetReason("Reason")
                .SetEmail("test@test.com")
                .SetStatus(CustomerStatus.ACTIVE)
                .SetName("Specific Name")
                .AddCustomProperty("FirstName", "Bob")

                .Build();

            Assert.Multiple(() =>
            {
                Assert.That(request, Is.Not.Null);
                Assert.That(request.Email, Is.EqualTo("test@test.com"));
                Assert.That(request.CustomerCode, Is.EqualTo("MOCK_CUSTOMER"));
                Assert.That(request.TrustLevel, Is.EqualTo("Trusted"));
                Assert.That(request.Status, Is.EqualTo("ACTIVE"));
                Assert.That(request.Name, Is.EqualTo("Specific Name"));
                Assert.That(request.CountryCode, Is.Null);
                Assert.That(request.BankAccounts, Has.One.Property("IdentifiedBankAccountName").EqualTo("Test_Name") &
                    Has.One.Property("BankAccountNumber").EqualTo("NLABN12345") &
                    Has.One.Property("Bank").Property("BankIBANCode").EqualTo("123"));
                Assert.That(request.Data, Is.Not.Null);
            });
        }
    }
}