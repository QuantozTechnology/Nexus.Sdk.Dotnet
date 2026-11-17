using System.Text.Json;
using Nexus.Sdk.Shared.Requests;

namespace Nexus.Sdk.Shared.Tests;

public class CustomerRequestTests
{
    [Test]
    public void UpdateCustomerRequest_SerializesFieldsToClear()
    {
        var request = new UpdateCustomerRequest
        {
            CustomerCode = "CUSTOMER01",
            FieldsToClear = ["Email", "PrimaryInternalAccountCode"]
        };

        using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
        var fieldsToClear = document.RootElement.GetProperty("fieldsToClear");

        Assert.That(fieldsToClear.EnumerateArray().Select(field => field.GetString()), Is.EqualTo(new[] { "Email", "PrimaryInternalAccountCode" }));
    }
    
    [Test]
    public void UpdateCustomerRequest_SerializesPrimaryInternalAccountCode()
    {
        var request = new UpdateCustomerRequest
        {
            CustomerCode = "CUSTOMER01",
            PrimaryInternalAccountCode = "INTERNAL01"
        };

        using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));

        Assert.That(document.RootElement.GetProperty("primaryInternalAccountCode").GetString(), Is.EqualTo("INTERNAL01"));
    }
}
