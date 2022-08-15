using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record TaxonomyResponse
    {
        [JsonConstructor]
        public TaxonomyResponse(string taxonomySchemaCode, string taxonomySchemaName,
            string taxonomySchemaDescription, string tokenCode, string validatedTaxonomy, string hash)
        {
            TaxonomySchemaCode = taxonomySchemaCode;
            TaxonomySchemaName = taxonomySchemaName;
            TaxonomySchemaDescription = taxonomySchemaDescription;
            TokenCode = tokenCode;
            ValidatedTaxonomy = validatedTaxonomy;
            Hash = hash;
        }

        [JsonPropertyName("taxonomySchemaCode")]
        public string TaxonomySchemaCode { get; }

        [JsonPropertyName("taxonomySchemaName")]
        public string TaxonomySchemaName { get; }

        [JsonPropertyName("taxonomySchemaDescription")]
        public string TaxonomySchemaDescription { get; }

        [JsonPropertyName("tokenCode")]
        public string TokenCode { get; }

        [JsonPropertyName("validatedTaxonomy")]
        public string ValidatedTaxonomy { get; }

        [JsonPropertyName("hash")]
        public string Hash { get; }
    }

    public record TaxonomySchemaResponse
    {
        [JsonConstructor]
        public TaxonomySchemaResponse(string code, string name, string description,
            string schema, string createdOn, string updatedOn)
        {
            Code = code;
            Name = name;
            Description = description;
            Schema = schema;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
        }

        [JsonPropertyName("code")]
        public string Code { get; }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("schema")]
        public string Schema { get; }

        [JsonPropertyName("createdDate")]
        public string CreatedOn { get; }

        [JsonPropertyName("updatedDate")]
        public string UpdatedOn { get; }
    }
}
