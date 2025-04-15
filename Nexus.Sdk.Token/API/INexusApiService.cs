using Nexus.Sdk.Token.API.Models;
using Nexus.Sdk.Token.API.Models.Response;

namespace Nexus.Sdk.Token.API;

public interface INexusApiService
{
    Task<CustomResultHolder> CreateDocumentStore(Dictionary<string, string> queryParams);

    Task<CustomResultHolder<DocumentStoreSettingsResponse>> GetDocumentStore();
}