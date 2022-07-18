using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Requests;

public record FundingOperationRequest
{
    [JsonPropertyName("accountCode")]
    public string? AccountCode { get; set; }

    [JsonPropertyName("paymentMethodCode")]
    public string? PaymentMethodCode { get; set; }

    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("tokenFundings")]
    public FundingDefinition[]? Definitions { get; set; }
}

public record FundingDefinition
{
    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    public FundingDefinition(string tokenCode, decimal amount)
    {
        TokenCode = tokenCode;
        Amount = amount;
    }
}

public record PaymentOperationRequest
{
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("payments")]
    public PaymentDefinition[] Definitions { get; set; }

    public PaymentOperationRequest(PaymentDefinition[] definitions, string? memo)
    {
        Definitions = definitions;
        Memo = memo;
    }
}

public record PaymentDefinition
{
    [JsonPropertyName("sender")]
    public string SenderPublicKey { get; set; }

    [JsonPropertyName("receiver")]
    public string ReceiverPublicKey { get; set; }

    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    public PaymentDefinition(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount)
    {
        SenderPublicKey = senderPublicKey;
        ReceiverPublicKey = receiverPublicKey;
        TokenCode = tokenCode;
        Amount = amount;
    }
}

public record PayoutOperationRequest
{
    [JsonPropertyName("accountCode")]
    public string? AccountCode { get; set; }

    [JsonPropertyName("paymentMethodCode")]
    public string? PaymentMethodCode { get; set; }

    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("tokenCode")]
    public string? TokenCode { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}