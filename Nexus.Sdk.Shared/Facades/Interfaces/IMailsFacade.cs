using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public interface IMailsFacade
{
    /// <summary>
    /// List Mails based on query paramaters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Paged list of Partner's mails
    /// </returns>
    public Task<PagedResponse<MailsResponse>> Get(IDictionary<string, string>? query);

    /// <summary>
    /// Create a new mail
    /// </summary>
    /// The minimum requirements are the type, 1 reference and the main recipient
    /// </remarks>
    /// <returns>
    /// Created Mail details
    /// </returns>
    public Task<MailsResponse> Create(CreateMailRequest request);

    /// <summary>
    /// Update Mail status to Sent
    /// </summary>
    /// <param name="code">Unique identifier of mail</param>
    /// <returns>
    /// Updated Mail Response
    /// </returns>
    public Task<MailsResponse> UpdateMailSent(string code);
}
