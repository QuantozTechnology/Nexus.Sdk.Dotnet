using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public interface ITaxonomyFacade
    {
        public Task<TaxonomySchemaResponse> CreateSchema(string code, string schema, string? name = null, string? description = null);

        public Task<TaxonomySchemaResponse> GetSchema(string taxonomySchemaCode);

        public Task<TaxonomySchemaResponse> UpdateSchema(string taxonomySchemaCode, string? name = null, string? description = null, string? schema = null);

        public Task<TaxonomyResponse> Get(string tokenCode);
    }
}
