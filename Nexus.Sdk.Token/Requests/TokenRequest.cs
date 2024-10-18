using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

public record StellarTokenRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "XLM";

    [JsonPropertyName("stellar")]
    public StellarTokens? StellarTokens { get; set; }

    [JsonPropertyName("data")]
    public IDictionary<string, string> Data = new Dictionary<string, string>();
}

public record AlgorandTokenRequest
{
    [JsonPropertyName("cryptoCode")]
    public string CryptoCode { get; set; } = "ALGO";

    [JsonPropertyName("algorand")]
    public AlgorandTokens? AlgorandTokens { get; set; }

    [JsonPropertyName("data")]
    public IDictionary<string, string> Data = new Dictionary<string, string>();
}

public record AlgorandTokens
{
    [JsonPropertyName("tokens")]
    public IEnumerable<AlgorandTokenDefinition>? Definitions { get; set; }

    [JsonPropertyName("algorandSettings")]
    public AlgorandTokenSettings? AlgorandSettings { get; set; }
}

public record StellarTokenSettings
{
    [JsonPropertyName("authorizationRequired")]
    public bool AuthorizationRequired { get; set; } = true;

    [JsonPropertyName("authorizationRevocable")]
    public bool AuthorizationRevocable { get; set; } = true;

    [JsonPropertyName("clawbackEnabled")]
    public bool ClawbackEnabled { get; set; } = true;
}

public record AlgorandTokenSettings
{
    [JsonPropertyName("clawbackEnabled")]
    public bool ClawbackEnabled { get; set; } = true;

    [JsonPropertyName("authorizationRevocable")]
    public bool AuthorizationRevocable { get; set; } = true;

    [JsonPropertyName("authorizationRequired")]
    public bool AuthorizationRequired { get; set; } = true;
}

public record StellarTokens
{
    [JsonPropertyName("tokens")]
    public IEnumerable<StellarTokenDefinition>? Definitions { get; set; }

    [JsonPropertyName("stellarSettings")]
    public StellarTokenSettings? StellarSettings { get; set; }
}

public record TokenDefinition
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("tokenType")]
    public string TokenType { get; set; }

    [JsonPropertyName("rate")]
    public decimal? Rate { get; set; }

    [JsonPropertyName("peggedBy")]
    public string? PeggedBy { get; set; }

    [JsonPropertyName("taxonomy")]
    public TaxonomyRequest? Taxonomy { get; set; } = null;

    protected TokenDefinition(string code, string name)
    {
        Code = code;
        Name = name;
        TokenType = "PeggedByAsset";
        Rate = null;
    }

    protected TokenDefinition(string code, string name, string peggedBy, decimal rate)
    {
        Code = code;
        Name = name;
        TokenType = "PeggedByCurrency";
        Rate = rate;
        PeggedBy = peggedBy;
    }

    public void SetTaxonomy(TaxonomyRequest taxonomy)
    {
        Taxonomy = taxonomy;
    }
}

public record StellarTokenDefinition  : TokenDefinition
{
    [JsonPropertyName("settings")]
    public StellarTokenDefinitionSettings Settings { get; set; }

    private StellarTokenDefinition(string code, string name, decimal? accountLimit)
        : base(code, name)
    {
        Settings = new StellarTokenDefinitionSettings(accountLimit);
    }

    private StellarTokenDefinition(string code, string name, string peggedBy, decimal rate, decimal? accountLimit)
        : base(code, name, peggedBy, rate)
    {
        Settings = new StellarTokenDefinitionSettings(accountLimit);
    }

    public static StellarTokenDefinition TokenizedAsset(string code, string name, decimal? accountLimit = null)
    {
        return new StellarTokenDefinition(code, name, accountLimit);
    }

    public static StellarTokenDefinition StableCoin(string code, string name, string peggedBy, decimal rate, decimal? accountLimit = null)
    {
        return new StellarTokenDefinition(code, name, peggedBy, rate, accountLimit);
    }
}

public record AlgorandTokenDefinition : TokenDefinition
{
    [JsonPropertyName("settings")]
    public AlgorandTokenDefinitionSettings Settings { get; set; }

    private AlgorandTokenDefinition(string code, string name, decimal totalSupply, decimal decimals)
    : base(code, name)
    {
        Settings = new AlgorandTokenDefinitionSettings(totalSupply, decimals);
    }

    private AlgorandTokenDefinition(string code, string name, string peggedBy, decimal rate,
        decimal totalSupply, decimal decimals)
        : base(code, name, peggedBy, rate)
    {
        Settings = new AlgorandTokenDefinitionSettings(totalSupply, decimals);
    }

    public static AlgorandTokenDefinition TokenizedAsset(string code, string name, decimal totalSupply, decimal decimals)
    {
        return new AlgorandTokenDefinition(code, name, totalSupply, decimals);
    }

    public static AlgorandTokenDefinition StableCoin(string code, string name, string peggedBy, decimal rate,
        decimal totalSupply, decimal decimals)
    {
        return new AlgorandTokenDefinition(code, name, peggedBy, rate, totalSupply, decimals);
    }
}

public record StellarTokenDefinitionSettings
{
    [JsonPropertyName("accountLimit")]
    public decimal? AccountLimit { get; set; }

    [JsonPropertyName("decimals")]
    public int Decimals { get; } = 7;

    public StellarTokenDefinitionSettings(decimal? accountLimit)
    {
        AccountLimit = accountLimit;
    }
}

public record AlgorandTokenDefinitionSettings
{
    [JsonPropertyName("overallLimit")]
    public decimal TotalSupply { get; set; }

    [JsonPropertyName("decimals")]
    public decimal Decimals { get; set; }

    public AlgorandTokenDefinitionSettings(decimal totalSupply, decimal decimals)
    {
        TotalSupply = totalSupply;
        Decimals = decimals;
    }
}

public record TaxonomyRequest
{
    [JsonPropertyName("taxonomySchemaCode")]
    public string? SchemaCode { get; set; }

    [JsonPropertyName("assetUrl")]
    public string? AssetUrl { get; set; }

    [JsonPropertyName("properties")]
    public IDictionary<string, object>? Properties { get; set; }

    [JsonPropertyName("taxonomyHash")]
    public string? TaxonomyHash { get; set; }

    public TaxonomyRequest(string schemaCode, string? assetUrl)
    {
        SchemaCode = schemaCode;
        AssetUrl = assetUrl;
        Properties = new Dictionary<string, object>();
    }

    public TaxonomyRequest(string schemaCode, string? assetUrl, IDictionary<string, object> properties)
    {
        SchemaCode = schemaCode;
        AssetUrl = assetUrl;
        Properties = properties;
    }

    public TaxonomyRequest(string? schemaCode, string? assetUrl, IDictionary<string, object>? properties, string taxonomyHash)
    {
        SchemaCode = schemaCode;
        AssetUrl = assetUrl;
        Properties = properties;
        TaxonomyHash = taxonomyHash;
    }

    public void AddProperty(string key, object value)
    {
        Properties?.Add(key, value);
    }
}