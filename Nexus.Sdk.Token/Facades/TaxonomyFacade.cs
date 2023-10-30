using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public class TaxonomyFacade : TokenServerFacade, ITaxonomyFacade
    {
        public TaxonomyFacade(ITokenServerProvider provider) : base(provider) { }

        public async Task<TaxonomySchemaResponse> CreateSchema(string code, string schema, string? name = null, string? description = null)
        {
            return await _provider.CreateTaxonomySchema(code, schema, name, description);
        }

        public async Task<TaxonomySchemaResponse> GetSchema(string taxonomySchemaCode)
        {
            return await _provider.GetTaxonomySchema(taxonomySchemaCode);
        }

        public async Task<TaxonomySchemaResponse> UpdateSchema(string taxonomySchemaCode, string? name = null, string? description = null,
            string? schema = null)
        {
            return await _provider.UpdateTaxonomySchema(taxonomySchemaCode, name, description, schema);
        }

        public async Task<TaxonomyResponse> Get(string tokenCode)
        {
            return await _provider.GetTaxonomy(tokenCode);
        }
    }
}
