﻿using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses
{
    public record TokenOperationResponse
    {
        [JsonConstructor]
        public TokenOperationResponse(string code, string hash, OperationAccountResponses senderAccount, OperationAccountResponses receiverAccount, decimal amount, string created, string? finished, string status, string type, string? memo, string? message, string cryptoCode, string tokenCode, string paymentReference, decimal? fiatAmount, decimal? netFiatAmount, string blockchainTransactionId, OperationFees? fees, string? bankAccountNumber, string? nonce)
        {
            Code = code;
            Hash = hash;
            SenderAccount = senderAccount;
            ReceiverAccount = receiverAccount;
            Amount = amount;
            Created = created;
            Finished = finished;
            Status = status;
            Type = type;
            Memo = memo;
            Message = message;
            CryptoCode = cryptoCode;
            TokenCode = tokenCode;
            PaymentReference = paymentReference;
            FiatAmount = fiatAmount;
            NetFiatAmount = netFiatAmount;
            BlockchainTransactionId = blockchainTransactionId;
            Fees = fees;
            BankAccountNumber = bankAccountNumber;
            Nonce = nonce;
        }

        [JsonPropertyName("code")]
        public string Code { get; private set; }

        [JsonPropertyName("hash")]
        public string Hash { get; private set; }

        [JsonPropertyName("senderAccount")]
        public OperationAccountResponses SenderAccount { get; private set; }

        [JsonPropertyName("receiverAccount")]
        public OperationAccountResponses ReceiverAccount { get; private set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; private set; }

        [JsonPropertyName("created")]
        public string Created { get; private set; }

        [JsonPropertyName("finished")]
        public string? Finished { get; private set; }

        [JsonPropertyName("status")]
        public string Status { get; private set; }

        [JsonPropertyName("type")]
        public string Type { get; private set; }

        [JsonPropertyName("memo")]
        public string? Memo { get; private set; }

        [JsonPropertyName("message")]
        public string? Message { get; private set; }

        [JsonPropertyName("cryptoCode")]
        public string CryptoCode { get; private set; }

        [JsonPropertyName("tokenCode")]
        public string TokenCode { get; private set; }

        [JsonPropertyName("paymentReference")]
        public string PaymentReference { get; set; }

        [JsonPropertyName("fiatAmount")]
        public decimal? FiatAmount { get; private set; }

        [JsonPropertyName("netFiatAmount")]
        public decimal? NetFiatAmount { get; private set; }

        [JsonPropertyName("blockchainTransactionId")]
        public string BlockchainTransactionId { get; set; }

        [JsonPropertyName("fees")]
        public OperationFees? Fees { get; set; }

        [JsonPropertyName("bankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        [JsonPropertyName("nonce")]
        public string? Nonce { get; set; }
    }

    public class OperationFees
    {
        [JsonPropertyName("bankFees")]
        public OperationBankFees? BankFees { get; set; }

        [JsonPropertyName("partnerFees")]
        public OperationPartnerFees? PartnerFees { get; set; }

        [JsonPropertyName("networkFees")]
        public OperationNetworkFees? NetworkFees { get; set; }
    }

    public class OperationPartnerFees
    {
        [JsonPropertyName("totalFiat")]
        public decimal? TotalFiat { get; set; }
    }

    public class OperationNetworkFees
    {
        [JsonPropertyName("estimatedFiat")]
        public decimal? EstimatedFiat { get; set; }

        [JsonPropertyName("estimatedCrypto")]
        public decimal? EstimatedCrypto { get; set; }
    }

    public class OperationBankFees
    {
        [JsonPropertyName("totalFiat")]
        public decimal? TotalFiat { get; set; }
    }
}
