using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Requests
{
    public record CancelOrderRequest
    {
        public CancelOrderRequest(string orderCode)
        {
            OrderCode = orderCode;
        }

        [JsonPropertyName("tokenOrderCode")]
        public string OrderCode { get; set; }
    }

    public record OrderRequest
    {
        [JsonPropertyName("accountCode")]
        public string AccountCode { get; set; }

        [JsonPropertyName("buying")]
        public BuyOrderAmountDefinition? Buying { get; set; }

        [JsonPropertyName("selling")]
        public SellOrderAmountDefinition? Selling { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; } = "Limit";

        [JsonPropertyName("memo")]
        public string? Memo { get; set; }

        [JsonPropertyName("callbackUrl")]
        public string? CallbackUrl { get; set; }

        public OrderRequest(string accountCode, string? callbackUrl = null, string? memo = null)
        {
            AccountCode = accountCode;
            CallbackUrl = callbackUrl;
            Memo = memo;
        }
    }

    public record BuyOrderAmountDefinition
    {
        [JsonPropertyName("tokenCode")]
        public string TokenCode { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        public BuyOrderAmountDefinition(string tokenCode, decimal amount)
        {
            TokenCode = tokenCode;
            Amount = amount;
        }
    }

    public record SellOrderAmountDefinition
    {
        [JsonPropertyName("tokenCode")]
        public string TokenCode { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        public SellOrderAmountDefinition(string tokenCode, decimal amount)
        {
            TokenCode = tokenCode;
            Amount = amount;
        }
    }

    public class OrderRequestBuilder
    {
        private OrderRequest _orderRequest;

        public OrderRequestBuilder(string accountCode, string? callbackUrl = null, string? memo = null)
        {
            _orderRequest = new OrderRequest(accountCode, callbackUrl, memo);
        }

        public OrderRequestBuilder Buy(decimal amount, string tokenCode)
        {
            _orderRequest.Action = "Buy";
            _orderRequest.Buying = new BuyOrderAmountDefinition(tokenCode, amount);

            return this;
        }

        public OrderRequestBuilder Sell(decimal amount, string tokenCode)
        {
            _orderRequest.Action = "Sell";
            _orderRequest.Selling = new SellOrderAmountDefinition(tokenCode, amount);

            return this;
        }

        public OrderRequest For(decimal amount, string tokenCode)
        {
            if (string.IsNullOrWhiteSpace(_orderRequest.Action))
            {
                throw new InvalidOperationException("You must first call on Buy or Sell");
            }

            if (_orderRequest.Action == "Buy")
            {
                _orderRequest.Selling = new SellOrderAmountDefinition(tokenCode, amount);
            }

            if (_orderRequest.Action == "Sell")
            {
                _orderRequest.Buying = new BuyOrderAmountDefinition(tokenCode, amount);
            }

            return _orderRequest;
        }
    }
}
