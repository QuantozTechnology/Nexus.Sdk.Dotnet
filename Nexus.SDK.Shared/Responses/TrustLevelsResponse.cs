using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses
{
    public record TrustLevelsResponse
    {
        [JsonConstructor]
        public TrustLevelsResponse(string? name, string? description, bool? isActive, bool? requireExecutedSellDoesNotExceedLifetimeBuy, DetailedLimits? buyLimits, DetailedLimits? sellLimits, OverallLimits? overallLimits)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            RequireExecutedSellDoesNotExceedLifetimeBuy = requireExecutedSellDoesNotExceedLifetimeBuy;
            BuyLimits = buyLimits;
            SellLimits = sellLimits;
            OverallLimits = overallLimits;
        }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("requireExecutedSellDoesNotExceedLifetimeBuy")]
        public bool? RequireExecutedSellDoesNotExceedLifetimeBuy { get; set; }

        [JsonPropertyName("buyLimits")]
        public DetailedLimits? BuyLimits { get; set; }

        [JsonPropertyName("sellLimits")]
        public DetailedLimits? SellLimits { get; set; }

        [JsonPropertyName("overallLimits")]
        public OverallLimits? OverallLimits { get; set; }
    }

    public class DetailedLimits
    {
        [JsonConstructor]
        public DetailedLimits(decimal? dailyLimit, decimal? monthlyLimit, decimal? yearLimit, decimal? lifetimeLimit)
        {
            DailyLimit = dailyLimit;
            MonthlyLimit = monthlyLimit;
            YearLimit = yearLimit;
            LifetimeLimit = lifetimeLimit;
        }

        [JsonPropertyName("dailyLimit")]
        public decimal? DailyLimit { get; set; }

        [JsonPropertyName("monthlyLimit")]
        public decimal? MonthlyLimit { get; set; }

        [JsonPropertyName("yearLimit")]
        public decimal? YearLimit { get; set; }

        [JsonPropertyName("lifetimeLimit")]
        public decimal? LifetimeLimit { get; set; }
    }

    public class OverallLimits
    {
        [JsonConstructor]
        public OverallLimits(decimal? tokenBalanceLimit)
        {
            TokenBalanceLimit = tokenBalanceLimit;
        }

        [JsonPropertyName("tokenBalanceLimit")]
        public decimal? TokenBalanceLimit { get; set; }
    }
}
