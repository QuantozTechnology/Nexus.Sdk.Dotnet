using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public class MailsFacade : ServerFacade, IMailsFacade
{
    public MailsFacade(IServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// List Mails based on query paramaters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Paged list of Partner's mails
    /// </returns>
    public async Task<PagedResponse<MailsResponse>> Get(IDictionary<string, string>? query)
    {
        return await _provider.GetMails(query);
    }

    /// <summary>
    /// Update Mail status to Sent based on the code
    /// </summary>
    /// <param name="code">Mail code</param>
    /// <returns>
    /// Updated Mail record
    /// </returns>
    public async Task<MailsResponse> UpdateMailSent(string code)
    {
        return await _provider.UpdateMailSent(code);
    }
}
