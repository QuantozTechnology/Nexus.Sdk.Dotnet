using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record OrderResponse
    {
        [JsonPropertyName("accountCode")]
        public string AccountCode { get; set; }

        [JsonPropertyName("customerCode")]
        public string CustomerCode { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("blockchainCode")]
        public string BlockchainCode { get; set; }

        [JsonPropertyName("buying")]
        public OrderAmountResponse Buying { get; set; }

        [JsonPropertyName("selling")]
        public OrderAmountResponse Selling { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("expiring")]
        public DateTime? Expiring { get; set; }

        [JsonPropertyName("finished")]
        public DateTime? Finished { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("isPartial")]
        public bool IsPartial { get; set; }

        [JsonPropertyName("memo")]
        public string Memo { get; set; }

        [JsonPropertyName("callbackUrl")]
        public string CallbackUrl { get; set; }

        [JsonConstructor]
        public OrderResponse(string accountCode, string customerCode, string code,
            string blockchainCode, OrderAmountResponse buying, OrderAmountResponse selling,
            string action, string type, string status, DateTime created, DateTime? expiring,
            DateTime? finished, DateTime updated, bool isPartial, string memo, string callbackUrl)
        {
            AccountCode = accountCode;
            CustomerCode = customerCode;
            Code = code;
            BlockchainCode = blockchainCode;
            Buying = buying;
            Selling = selling;
            Action = action;
            Type = type;
            Status = status;
            Created = created;
            Expiring = expiring;
            Finished = finished;
            Updated = updated;
            IsPartial = isPartial;
            Memo = memo;
            CallbackUrl = callbackUrl;
        }
    }

    public record OrderAmountResponse
    {
        [JsonConstructor]
        public OrderAmountResponse(string tokenCode, decimal amount, decimal? executedAmount)
        {
            TokenCode = tokenCode;
            Amount = amount;
            ExecutedAmount = executedAmount;
        }

        [JsonPropertyName("tokenCode")]
        public string TokenCode { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("executedAmount")]
        public decimal? ExecutedAmount { get; set; }
    }

    public record CreateOrderResponse : SignableResponse
    {
        [JsonPropertyName("tokenOrder")]
        public required OrderResponse CreatedOrder { get; set; }
    }
}
