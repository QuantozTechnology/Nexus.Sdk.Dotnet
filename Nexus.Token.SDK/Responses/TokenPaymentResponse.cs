using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record TokenPaymentResponse
    {
        public TokenPaymentResponse(string code, string hash, AccountResponse senderAccount, AccountResponse receiverAccount, decimal amount, string created, string finished, string status, string type, string memo, string cryptoCode)
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
        public string Code { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("senderAccount")]
        public AccountResponse SenderAccount { get; set; }

        [JsonPropertyName("receiverAccount")]
        public AccountResponse ReceiverAccount { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("finished")]
        public string Finished { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("memo")]
        public string Memo { get; set; }

        [JsonPropertyName("cryptoCode")]
        public string CryptoCode { get; set; }
    }
}
