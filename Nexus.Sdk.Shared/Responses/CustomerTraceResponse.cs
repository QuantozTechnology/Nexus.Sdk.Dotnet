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

    [JsonPropertyName("IP")]
    public string IP { get; set; }

    [JsonPropertyName("Created ")]
    public string Created { get; set; }

    [JsonPropertyName("Action")]
    public string Action { get; set; }

    [JsonPropertyName("EntityType ")]
    public string EntityType { get; set; }

    [JsonPropertyName("GeoLocation")]
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
    [JsonPropertyName("CountryCode")]
    public string CountryCode { get; set; }

    [JsonPropertyName("CountryName")]
    public string CountryName { get; set; }

    [JsonPropertyName("ISP")]
    public string ISP { get; set; }
}