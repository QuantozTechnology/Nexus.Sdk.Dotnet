using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses
{
    public record TrustLevelsResponse
    {
        [JsonConstructor]
        public TrustLevelsResponse(string name, string? description, bool isActive, Limits? buyLimits, Limits? sellLimits, OverallLimits? overallLimits)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            BuyLimits = buyLimits;
            SellLimits = sellLimits;
            OverallLimits = overallLimits;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("buyLimits")]
        public Limits? BuyLimits { get; set; }

        [JsonPropertyName("sellLimits")]
        public Limits? SellLimits { get; set; }

        [JsonPropertyName("overallLimits")]
        public OverallLimits? OverallLimits { get; set; }
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
        public OverallLimits(double? custodianLimit, decimal? tokenBalanceLimit)
        {
            CustodianLimit = custodianLimit;
            TokenBalanceLimit = tokenBalanceLimit;
        }

        [JsonPropertyName("custodianLimit")]
        public double? CustodianLimit { get; set; }

        [JsonPropertyName("tokenBalanceLimit")]
        public decimal? TokenBalanceLimit { get; set; }
    }
}
