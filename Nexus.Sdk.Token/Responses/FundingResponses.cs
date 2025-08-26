using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses
{
    public record FundingResponses
    {
        [JsonPropertyName("paymentMethod")]
        public PaymentMethodInfo? PaymentMethod { get; set; }

        [JsonPropertyName("payments")]
        public required IEnumerable<FundingResponse> Funding { get; set; }

        [JsonPropertyName("transactionEnvelope")]
        public TxEnvelopeResponse? TransactionEnvelope { get; set; }
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

        [JsonPropertyName("bankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }

        [JsonPropertyName("blockchainTransactionId")]
        public string? BlockchainTransactionId { get; set; }
    }

    public class TxEnvelopeResponse
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("hash")]
        public string? Hash { get; set; }

        [JsonPropertyName("signedTransactionEnvelope")]
        public string? SignedTransactionEnvelope { get; set; }

        [JsonPropertyName("signingNeeded")]
        public bool SigningNeeded { get; set; }

        [JsonPropertyName("transactionEnvelopeStatus")]
        public string? Status { get; set; }

        [JsonPropertyName("validUntil")]
        public string? ValidUntil { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("created")]
        public string? Created { get; set; }

        [JsonPropertyName("memo")]
        public string? Memo { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}