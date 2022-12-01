using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record OperationAccountResponses
    {
        [JsonConstructor]
        public OperationAccountResponses(string customerCode, string accountCode, string publicKey)
        {
            CustomerCode = customerCode;
            AccountCode = accountCode;
            PublicKey = publicKey;
        }

        [JsonPropertyName("customerCode")]
        public string CustomerCode { get; }

        [JsonPropertyName("accountCode")]
        public string AccountCode { get; }

        [JsonPropertyName("accountAddress")]
        public string PublicKey { get; set; }
    }
}
