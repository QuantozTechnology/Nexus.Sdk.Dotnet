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
    public string TokenCode { get;  set; }

    [JsonPropertyName("limit")]
    public decimal Limit { get;  set; }

    [JsonPropertyName("limitReasons")]
    public string[] LimitReasons { get;  set; }

    [JsonPropertyName("remaining")]
    public TrustlevelLimit Remaining { get;  set; }

    [JsonPropertyName("total")]
    public TrustlevelLimit Total { get;  set; }
}

public record TrustlevelLimit
{
    [JsonConstructor]
    public TrustlevelLimit(decimal dailyLimit, decimal monthlyLimit, decimal? yearlyLimit, decimal? lifetimeLimit)
    {
        DailyLimit = dailyLimit;
        MonthlyLimit = monthlyLimit;
        YearlyLimit = yearlyLimit;
        LifetimeLimit = lifetimeLimit;
    }

    [JsonPropertyName("dailyLimit")]
    public decimal DailyLimit { get;  set; }

    [JsonPropertyName("monthlyLimit")]
    public decimal MonthlyLimit { get;  set; }

    [JsonPropertyName("yearlyLimit")]
    public decimal? YearlyLimit { get;  set; }

    [JsonPropertyName("lifetimeLimit")]
    public decimal? LifetimeLimit { get;  set; }
}