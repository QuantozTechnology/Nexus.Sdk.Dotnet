using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Responses;

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

public class TokenBalancesResponse
{
    [JsonPropertyName("tokenCode")]
    public string TokenCode { get; }

    [JsonPropertyName("issued")]
    public double Issued { get; }

    [JsonPropertyName("deleted")]
    public double Deleted { get; }

    [JsonPropertyName("available")]
    public double Available { get; }
    
    [JsonPropertyName("virtual")]
    public double? VirtualBalance { get; }

    [JsonPropertyName("total")]
    public double Total { get; }

    [JsonPropertyName("updated")]
    public string Updated { get; }

    [JsonConstructor]
    public TokenBalancesResponse(string tokenCode, double issued, double deleted, double available, double total, string updated, double? virtualBalance = null)
    {
        TokenCode = tokenCode;
        Issued = issued;
        Deleted = deleted;
        Available = available;
        Total = total;
        Updated = updated;
        VirtualBalance = virtualBalance;
    }
}

public class TokenFeePayerResponse
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; }

    [JsonPropertyName("feePayerAccountsTotals")]
    public FeePayerAccountsTotals FeePayerAccountsTotals { get; }

    [JsonConstructor]
    public TokenFeePayerResponse(string cryptoCode, FeePayerAccountsTotals feePayerAccountsTotals)
    {
        CryptoCode = cryptoCode;
        FeePayerAccountsTotals = feePayerAccountsTotals;
    }
}

public class FeePayerAccountsTotals
{
    [JsonPropertyName("total")]
    public int Total { get; }

    [JsonPropertyName("available")]
    public int Available { get; }

    [JsonPropertyName("locked")]
    public int Locked { get; }

    [JsonPropertyName("lowBalance")]
    public int LowBalance { get; }

    [JsonConstructor]
    public FeePayerAccountsTotals(int total, int available, int locked, int lowBalance)
    {
        Total = total;
        Available = available;
        Locked = locked;
        LowBalance = lowBalance;
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

public class FeePayerDetailsResponse
{
    [JsonPropertyName("address")]
    public required string Address { get; set; }
    
    [JsonPropertyName("cryptoCode")]
    public required string CryptoCode { get; set; }
    
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    
    [JsonPropertyName("balance")]
    public required decimal Balance { get; set; } = 0;
    
    [JsonPropertyName("updated")]
    public string? Updated { get; set; }
}


