using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests
{
    public record CreateTaxonomySchemaRequest
    {
        public CreateTaxonomySchemaRequest(string code, string schema)
        {
            Code = code;
            Schema = schema;
        }

        [JsonPropertyName("code")]
        public string Code { get; }

        [JsonPropertyName("schema")]
        public string Schema { get; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public record UpdateTaxonomySchemaRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("schema")]
        public string? Schema { get; set; }
    }
}
