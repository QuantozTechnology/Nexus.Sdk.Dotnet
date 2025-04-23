using Nexus.Sdk.Token.API.Models;
using Nexus.Sdk.Token.API.Models.Response;

namespace Nexus.Sdk.Token.API;

public interface INexusApiService
{
    Task<CustomResultHolder> CreateDocumentStore(DocumentStoreSettings documentStoreSettings);
    Task<CustomResultHolder<DocumentStoreSettingsResponse>> GetDocumentStore();
    Task<CustomResultHolder> DeleteDocumentStore();
    Task<CustomResultHolder<PagedResult<DocumentStoreItemResponse>>> GetDocumentStoreList(Dictionary<string, string> queryParams);
    Task<CustomResultHolder> AddDocumentToStore(DocumentFileUploadData documentFileUploadData);
    Task<Stream> GetDocumentFromStore(string filePath);
    Task<CustomResultHolder> DeleteDocumentFromStore(string filePath);
}