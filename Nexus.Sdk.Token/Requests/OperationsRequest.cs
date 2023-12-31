﻿using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record FundingOperationRequest
{
    [JsonPropertyName("customerCode")]
    public required string CustomerCode { get; set; }

    [JsonPropertyName("accountCode")]
    public required string AccountCode { get; set; }

    [JsonPropertyName("paymentMethodCode")]
    public required string PaymentMethodCode { get; set; }

    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("tokenFundings")]
    public required IEnumerable<FundingDefinition> Definitions { get; set; }
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
    public IEnumerable<PaymentDefinition> Definitions { get; set; }

    public PaymentOperationRequest(IEnumerable<PaymentDefinition> definitions, string? memo)
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