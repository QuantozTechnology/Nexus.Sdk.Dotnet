using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Facades.Interfaces;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public class DocumentStoreFacade : TokenServerFacade, IDocumentStoreFacade
    {
        public DocumentStoreFacade(ITokenServerProvider provider) : base(provider)
        {
        }

        public async Task AddDocumentToStore(FileUploadRequest fileUploadRequest, string customerIPAddress)
        {
            await _provider.AddDocumentToStore(fileUploadRequest, customerIPAddress);
        }

        public async Task CreateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            await _provider.CreateDocumentStore(documentStoreSettings, customerIPAddress);
        }

        public async Task DeleteDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            await _provider.DeleteDocumentFromStore(documentRequest, customerIPAddress);
        }

        public async Task DeleteDocumentStore(string customerIPAddress)
        {
            await _provider.DeleteDocumentStore(customerIPAddress);
        }

        public async Task<Stream> GetDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            return await _provider.GetDocumentFromStore(documentRequest, customerIPAddress);
        }

        public async Task<DocumentStoreSettingsResponse> GetDocumentStore(string customerIPAddress)
        {
            return await _provider.GetDocumentStore(customerIPAddress);
        }

        public async Task<PagedResponse<DocumentStoreItemResponse>> GetDocumentStoreFileList(IDictionary<string, string>? queryParameters, string customerIPAddress)
        {
            return await _provider.GetDocumentStoreFileList(queryParameters, customerIPAddress);
        }

        public async Task UpdateDocumentInStore(FileUpdateRequest fileUpdateRequest, string customerIPAddress)
        {
            await _provider.UpdateDocumentInStore(fileUpdateRequest, customerIPAddress);
        }

        public async Task UpdateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            await _provider.UpdateDocumentStore(documentStoreSettings, customerIPAddress);
        }
    }
}
