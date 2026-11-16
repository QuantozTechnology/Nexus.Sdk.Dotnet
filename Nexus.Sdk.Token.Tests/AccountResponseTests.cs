using System.Text.Json;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Tests;

public class AccountResponseTests
{
    [TestCase(true)]
    [TestCase(false)]
    public void AccountResponse_DeserializesIsPrimary(bool isPrimary)
    {
        var json = $$"""
            {
                "customerCode": "CUSTOMER01",
                "accountCode": "INTERNAL01",
                "dcCode": "INTERNAL",
                "customerCryptoAddress": "ADDRESS01",
                "accountStatus": "ACTIVE",
                "accountType": "MANAGED",
                "customName": "Internal account",
                "created": "2026-01-01T00:00:00Z",
                "tokenSettings": { "allowedTokens": [] },
                "isPrimary": {{isPrimary.ToString().ToLowerInvariant()}}
            }
            """;

        var response = JsonSerializer.Deserialize<AccountResponse>(json);

        Assert.That(response, Is.Not.Null);
        Assert.That(response!.IsPrimary, Is.EqualTo(isPrimary));
    }

    [Test]
    public void AccountResponse_DeserializesMissingIsPrimaryAsNull()
    {
        const string json = """
            {
                "customerCode": "CUSTOMER01",
                "accountCode": "EXTERNAL01",
                "dcCode": "XLM",
                "customerCryptoAddress": "ADDRESS01",
                "accountStatus": "ACTIVE",
                "accountType": "MANAGED",
                "customName": "External account",
                "created": "2026-01-01T00:00:00Z",
                "tokenSettings": { "allowedTokens": [] }
            }
            """;

        var response = JsonSerializer.Deserialize<AccountResponse>(json);

        Assert.That(response, Is.Not.Null);
        Assert.That(response!.IsPrimary, Is.Null);
    }
}
