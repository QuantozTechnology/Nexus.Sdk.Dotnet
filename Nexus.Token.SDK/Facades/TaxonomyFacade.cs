using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades
{
    public class TaxonomyFacade : TokenServerFacade
    {
        public TaxonomyFacade(ITokenServerProvider provider) : base(provider) { }

        public async Task<GetTaxonomySchemaResponse> CreateSchema(string code, string schema, string? name = null, string? description = null)
        {
            return await _provider.CreateTaxonomySchema(code, schema, name, description);
        }

        public async Task<GetTaxonomySchemaResponse> GetSchema(string taxonomySchemaCode)
        {
            return await _provider.GetTaxonomySchema(taxonomySchemaCode);
        }

        public async Task<GetTaxonomySchemaResponse> UpdateSchema(string taxonomySchemaCode, string? name = null, string? description = null,
            string? schema = null)
        {
            return await _provider.UpdateTaxonomySchema(taxonomySchemaCode, name, description, schema);
        }

        public async Task<GetTaxonomyResponse> Get(string tokenCode)
        {
            return await _provider.GetTaxonomy(tokenCode);
        }
    }
}
