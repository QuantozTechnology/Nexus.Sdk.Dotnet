using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses
{
    public record GetTaxonomyResponse
    {
        [JsonConstructor]
        public GetTaxonomyResponse(string taxonomySchemaCode, string taxonomySchemaName,
            string taxonomySchemaDescription, string tokenCode, string validatedTaxonomy, string hash)
        {
            TaxonomySchemaCode = taxonomySchemaCode;
            TaxonomySchemaName = taxonomySchemaName;
            TaxonomySchemaDescription = taxonomySchemaDescription;
            TokenCode = tokenCode;
            ValidatedTaxonomy = validatedTaxonomy;
            Hash = hash;
        }

        public string TaxonomySchemaCode { get; }

        public string TaxonomySchemaName { get; }

        public string TaxonomySchemaDescription { get; }

        public string TokenCode { get; }

        public string ValidatedTaxonomy { get; }

        public string Hash { get; }
    }

    public record GetTaxonomySchemaResponse
    {
        [JsonConstructor]
        public GetTaxonomySchemaResponse(string code, string name, string description,
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

        [JsonPropertyName("createdOn")]
        public string CreatedOn { get; }

        [JsonPropertyName("updatedOn")]
        public string UpdatedOn { get; }
    }
}
