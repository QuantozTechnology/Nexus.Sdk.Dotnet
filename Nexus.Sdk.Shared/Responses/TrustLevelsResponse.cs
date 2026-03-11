using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses
{
    public record TrustLevelsResponse
    {
        [JsonConstructor]
        public TrustLevelsResponse(string name, string? description, bool isActive, bool requireExecutedSellDoesNotExceedLifetimeBuy, decimal? monthlyExemptionAmount, Limits? buyLimits, Limits? sellLimits, OverallLimits? overallLimits, Dictionary<string, bool> flags)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            RequireExecutedSellDoesNotExceedLifetimeBuy = requireExecutedSellDoesNotExceedLifetimeBuy;
            MonthlyExemptionAmount = monthlyExemptionAmount;
            BuyLimits = buyLimits;
            SellLimits = sellLimits;
            OverallLimits = overallLimits;
            Flags = flags;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("requireExecutedSellDoesNotExceedLifetimeBuy")]
        public bool RequireExecutedSellDoesNotExceedLifetimeBuy { get; set; }

        [JsonPropertyName("monthlyExemptionAmount")]
        public decimal? MonthlyExemptionAmount { get; set; }

        [JsonPropertyName("buyLimits")]
        public Limits? BuyLimits { get; set; }

        [JsonPropertyName("sellLimits")]
        public Limits? SellLimits { get; set; }

        [JsonPropertyName("overallLimits")]
        public OverallLimits? OverallLimits { get; set; }

        [JsonPropertyName("flags")]
        public Dictionary<string, bool> Flags { get; set; }
    }

    public class Limits
    {
        [JsonConstructor]
        public Limits(decimal? dailyLimit, decimal? monthlyLimit, decimal? yearLimit, decimal? lifetimeLimit)
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
        public OverallLimits(decimal? tokenBalanceLimit)
        {
            TokenBalanceLimit = tokenBalanceLimit;
        }

        [JsonPropertyName("tokenBalanceLimit")]
        public decimal? TokenBalanceLimit { get; set; }
    }
}
