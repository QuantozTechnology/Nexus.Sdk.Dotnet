using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nexus.Sdk.Shared.Responses;

public record CustomerTraceResponse
{
    [JsonConstructor]
    public CustomerTraceResponse(string ip, string created, string action, string entityType, GeoLocationResponse geolocation)
    {
        IP= ip;
        Created= created;
        Action= action;
        EntityType= entityType;
        GeoLocation= geolocation;
    }

    [JsonPropertyName("ip")]
    public string IP { get; set; }

    [JsonPropertyName("created")]
    public string Created { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("entityType")]
    public string EntityType { get; set; }

    [JsonPropertyName("geoLocation")]
    public GeoLocationResponse GeoLocation { get; set; }

}

public class GeoLocationResponse
{
    [JsonConstructor]
    public GeoLocationResponse(string countryCode, string countryName, string isp)
    {
        CountryCode = countryCode;
        CountryName = countryName;
        ISP = isp;
    }
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("countryName")]
    public string CountryName { get; set; }

    [JsonPropertyName("isp")]
    public string ISP { get; set; }
}