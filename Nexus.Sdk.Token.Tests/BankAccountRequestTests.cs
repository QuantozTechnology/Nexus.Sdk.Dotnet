using System.Text.Json;
using Nexus.Sdk.Token.Requests;

namespace Nexus.Sdk.Token.Tests;

public class BankAccountRequestTests
{
    [Test]
    public void UpdateBankAccountRequest_SerializesFieldsToClear()
    {
        var request = new UpdateBankAccountRequest
        {
            Number = "NL00BANK0123456789",
            CustomerCode = "CUSTOMER01",
            CurrencyCode = "EUR",
            FieldsToClear = ["Name", "Bank"]
        };

        using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
        var fieldsToClear = document.RootElement.GetProperty("fieldsToClear");

        Assert.That(fieldsToClear.EnumerateArray().Select(field => field.GetString()), Is.EqualTo(new[] { "Name", "Bank" }));
    }
}
