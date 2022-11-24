using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses;

public record TokenLimitsResponse
{
    [JsonConstructor]
    public TokenLimitsResponse(string tokenCode, decimal limit, string[] limitReasons, TrustlevelLimit remaining, TrustlevelLimit total)
    {
        TokenCode = tokenCode;
        Limit = limit;
        LimitReasons = limitReasons;
        Remaining = remaining;
        Total = total;
    }

    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; private set; }

    [JsonPropertyName("limit")]
    public decimal Limit { get; private set; }

    [JsonPropertyName("limitReasons")]
    public string[] LimitReasons { get; private set; }

    [JsonPropertyName("remainingLimits")]
    public TrustlevelLimit Remaining { get; private set; }

    [JsonPropertyName("totalLimits")]
    public TrustlevelLimit Total { get; private set; }
}

public record TrustlevelLimit
{
    public TrustlevelLimit(decimal dailyLimit, decimal monthlyLimit, decimal? yearlyLimit, decimal? lifetimeLimit)
    {
        DailyLimit = dailyLimit;
        MonthlyLimit = monthlyLimit;
        YearlyLimit = yearlyLimit;
        LifetimeLimit = lifetimeLimit;
    }

    [JsonPropertyName("dailyLimit")]
    public decimal DailyLimit { get; private set; }

    [JsonPropertyName("monthlyLimit")]
    public decimal MonthlyLimit { get; private set; }

    [JsonPropertyName("yearlyLimit")]
    public decimal? YearlyLimit { get; private set; }

    [JsonPropertyName("lifetimeLimit")]
    public decimal? LifetimeLimit { get; private set; }
}