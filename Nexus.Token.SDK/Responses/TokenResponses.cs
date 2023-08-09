using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses;

public class TokenResponse
{
    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("issuerAddress")]
    public string IssuerAddress { get; }

    [JsonPropertyName("status")]
    public string Status { get; }

    [JsonPropertyName("created")]
    public string Created { get; }

    [JsonPropertyName("blockchainId")]
    public string BlockchainId { get; }

    [JsonConstructor]
    public TokenResponse(string code, string name, string issuerAddress, string status, string created, string blockchainId)
    {
        Code = code;
        Name = name;
        IssuerAddress = issuerAddress;
        Status = status;
        Created = created;
        BlockchainId = blockchainId;
    }
}

public class TokenDetailsResponse
{
    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("issuerAddress")]
    public string IssuerAddress { get; }

    [JsonPropertyName("status")]
    public string Status { get; }

    [JsonPropertyName("created")]
    public string Created { get; }

    [JsonPropertyName("blockchainId")]
    public string BlockchainId { get; }

    [JsonPropertyName("taxonomy")]
    public GetTaxonomyResponse Taxonomy { get; }

    [JsonPropertyName("data")]
    public IDictionary<string, string> Data { get; set; }

    [JsonConstructor]
    public TokenDetailsResponse(string code, string name, string issuerAddress, string status, string created, string blockchainId, GetTaxonomyResponse taxonomy, IDictionary<string, string> data)
    {
        Code = code;
        Name = name;
        IssuerAddress = issuerAddress;
        Status = status;
        Created = created;
        BlockchainId = blockchainId;
        Taxonomy = taxonomy;
        Data = data;
    }
}

public class GetTaxonomyResponse
{
    [JsonPropertyName("schemaCode")]
    public string TaxonomySchemaCode { get; set; }

    [JsonPropertyName("assetUrl")]
    public string AssetUrl { get; set; }

    [JsonPropertyName("taxonomyProperties")]
    public string TaxonomyProperties { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonConstructor]
    public GetTaxonomyResponse(string taxonomySchemaCode, string assetUrl, string taxonomyProperties, string hash)
    {
        TaxonomySchemaCode = taxonomySchemaCode;
        AssetUrl = assetUrl;
        TaxonomyProperties = taxonomyProperties;
        Hash = hash;
    }
}

public class CreateTokenResponse
{
    [JsonPropertyName("tokens")]
    public TokenResponse[] Tokens { get; }

    [JsonConstructor]
    public CreateTokenResponse(TokenResponse[] tokens)
    {
        Tokens = tokens;
    }

}
