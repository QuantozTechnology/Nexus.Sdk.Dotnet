﻿using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses
{
    public record PayoutOperationResponse
    {
        [JsonPropertyName("payout")]
        public required PayoutResponse Payout { get; set; }
    }

    public record PayoutResponse
    {
        [JsonPropertyName("tokenCode")]
        public required string TokenCode { get; set; }

        [JsonPropertyName("paymentMethodName")]
        public required string PaymentMethodName { get; set; }

        [JsonPropertyName("requestedAmount")]
        public required decimal RequestedAmount { get; set; }

        [JsonPropertyName("executedAmounts")]
        public required ExecutedAmounts ExecutedAmounts { get; set; }

        [JsonPropertyName("fees")]
        public Fees? Fees { get; set; }

        [JsonPropertyName("paymentReference")]
        public string? PaymentReference { get; set; }

        [JsonPropertyName("paymentCode")]
        public string? PaymentCode { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("bankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }
    }

    public record ExecutedAmounts
    {
        [JsonPropertyName("tokenAmount")]
        public required decimal TokenAmount { get; set; }

        [JsonPropertyName("fiatValue")]
        public decimal? FiatValue { get; set; }

        [JsonPropertyName("tokenRate")]
        public decimal? TokenRate { get; set; }
    }

    public record Fees
    {
        [JsonPropertyName("serviceFee")]
        public decimal? ServiceFee { get; set; }

        [JsonPropertyName("bankFee")]
        public decimal? BankFee { get; set; }

        [JsonPropertyName("serviceFeeInFiat")]
        public decimal? ServiceFeeInFiat { get; set; }

        [JsonPropertyName("bankFeeInFiat")]
        public decimal? BankFeeInFiat { get; set; }
    }
}
