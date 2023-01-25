namespace Nexus.Token.SDK.Responses
{
    public record PayoutOperationResponse
    {
        public required PayoutResponse Payout { get; set; }
    }

    public record PayoutResponse
    {
        public required string TokenCode { get; set; }

        public required string PaymentMethodName { get; set; }

        public required decimal RequestedAmount { get; set; }

        public required ExecutedAmounts ExecutedAmounts { get; set; }

        public Fees? Fees { get; set; }
    }

    public record ExecutedAmounts
    {
        public required decimal TokenAmount { get; set; }

        public decimal? FiatValue { get; set; }

        public decimal? TokenRate { get; set; }
    }

    public record Fees
    {
        public decimal? ServiceFee { get; set; }

        public decimal? BankFee { get; set; }

        public decimal? ServiceFeeInFiat { get; set; }

        public decimal? BankFeeInFiat { get; set; }
    }
}
