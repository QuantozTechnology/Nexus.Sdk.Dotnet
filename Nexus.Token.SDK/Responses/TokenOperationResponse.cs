﻿using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record TokenOperationResponse
    {
        [JsonConstructor]
        public TokenOperationResponse(string code, string hash, AccountResponse senderAccount, AccountResponse receiverAccount, decimal amount, string created, string finished, string status, string type, string memo, string cryptoCode)
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
            CryptoCode = cryptoCode;
        }

        [JsonPropertyName("code")]
        public string Code { get; private set; }

        [JsonPropertyName("hash")]
        public string Hash { get; private set; }

        [JsonPropertyName("senderAccount")]
        public AccountResponse SenderAccount { get; private set; }

        [JsonPropertyName("receiverAccount")]
        public AccountResponse ReceiverAccount { get; private set; }

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

        [JsonPropertyName("cryptoCode")]
        public string CryptoCode { get; private set; }
    }
}