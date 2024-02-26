using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses
{
    public record FundingResponses
    {
        [JsonPropertyName("paymentMethod")]
        public PaymentMethodInfo? PaymentMethod { get; set; }

        [JsonPropertyName("payments")]
        public required IEnumerable<FundingResponse> Funding { get; set; }
    }

    public class PaymentMethodInfo
    {
        [JsonPropertyName("paymentMethodName")]
        public string? PaymentMethodName { get; set; }
    }

    public record FundingResponse
    {
        [JsonPropertyName("tokenCode")]
        public required string TokenCode { get; set; }

        [JsonPropertyName("paymentCode")]
        public required string FundingPaymentCode { get; set; }

        [JsonPropertyName("requestedAmount")]
        public required decimal RequestedAmount { get; set; }

        [JsonPropertyName("executedAmount")]
        public required decimal ExecutedAmount { get; set; }

        [JsonPropertyName("serviceFee")]
        public decimal? ServiceFee { get; set; }

        [JsonPropertyName("bankFee")]
        public decimal? BankFee { get; set; }

        [JsonPropertyName("paymentReference")]
        public string? PaymentReference { get; set; }
    }
}
