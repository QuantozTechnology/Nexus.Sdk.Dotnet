using System.Text.Json;
using Nexus.Sdk.Token.Requests;

namespace Nexus.Sdk.Token.Tests;

public class AccountRequestTests
{
    [Test]
    public void UpdateTokenAccountRequest_SerializesFieldsToClear()
    {
        var request = new UpdateTokenAccountRequest
        {
            CustomName = "Account name",
            FieldsToClear = ["CustomName"]
        };

        using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
        var root = document.RootElement;

        Assert.That(root.GetProperty("customName").GetString(), Is.EqualTo("Account name"));
        Assert.That(root.GetProperty("fieldsToClear").EnumerateArray().Select(field => field.GetString()), Is.EqualTo(new[] { "CustomName" }));
    }
}
