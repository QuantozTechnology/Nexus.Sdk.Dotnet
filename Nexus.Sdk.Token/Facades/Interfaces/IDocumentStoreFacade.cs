using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades.Interfaces
{
    public interface IDocumentStoreFacade
    {
        /// <summary>
        /// Retrieve the Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task<DocumentStoreSettingsResponse> GetDocumentStore(string customerIPAddress);

        /// <summary>
        /// Create a new Document Store with the provided settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task CreateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress);

        /// <summary>
        /// Update the existing Document Store settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task UpdateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress);

        /// <summary>
        /// Delete the existing Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task DeleteDocumentStore(string customerIPAddress);

        /// <summary>
        /// Retrieve a list of files in the Document Store based on the provided query parameters.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task<PagedResponse<DocumentStoreItemResponse>> GetDocumentStoreFileList(IDictionary<string, string>? queryParameters, string customerIPAddress);

        /// <summary>
        /// Upload a document to the Document Store.
        /// </summary>
        /// <param name="fileUploadRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task AddDocumentToStore(FileUploadRequest fileUploadRequest, string customerIPAddress);

        /// <summary>
        /// Retrieve a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task<Stream> GetDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress);

        /// <summary>
        /// Delete a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task DeleteDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress);

        /// <summary>
        /// Update document metadata in the Document Store.
        /// </summary>
        /// <param name="fileUpdateRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public Task UpdateDocumentInStore(FileUpdateRequest fileUpdateRequest, string customerIPAddress);
    }
}
