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

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("tokenFundings")]
    public required IEnumerable<FundingDefinition> Definitions { get; set; }
}

public record FundingDefinition
{
    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("paymentReference")]
    public string? PaymentReference { get; set; }

    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    public FundingDefinition(string tokenCode, decimal amount, string? paymentReference, string? nonce)
    {
        TokenCode = tokenCode;
        Amount = amount;
        PaymentReference = paymentReference;
        Nonce = nonce;
    }
}

public record PaymentOperationRequest
{
    [JsonPropertyName("memo")]
    public string? Memo { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("cryptoCode")]
    public string? CryptoCode { get; set; }

    [JsonPropertyName("callbackUrl")]
    public string? CallbackUrl { get; set; }

    [JsonPropertyName("payments")]
    public IEnumerable<PaymentDefinition> Definitions { get; set; }

    public PaymentOperationRequest(IEnumerable<PaymentDefinition> definitions, string? memo, string? message, string? cryptoCode, string? callbackUrl)
    {
        Definitions = definitions;
        Memo = memo;
        Message = message;
        CryptoCode = cryptoCode;
        CallbackUrl = callbackUrl;
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

    [JsonPropertyName("blockchainTransactionId")]
    public string? BlockchainTransactionId { get; set; }

    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    public PaymentDefinition(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? blockchainTransactionId = null, string? nonce = null)
    {
        SenderPublicKey = senderPublicKey;
        ReceiverPublicKey = receiverPublicKey;
        TokenCode = tokenCode;
        Amount = amount;
        BlockchainTransactionId = blockchainTransactionId;
        Nonce = nonce;
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

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("tokenCode")]
    public string? TokenCode { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("paymentReference")]
    public string? PaymentReference { get; set; }

    [JsonPropertyName("blockchainTransactionId")]
    public string? BlockchainTransactionId { get; set; }

    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }
}

public class UpdateOperationStatusRequest
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; } = "Operation updated";

    [JsonPropertyName("paymentReference")]
    public string? PaymentReference { get; set; }
}