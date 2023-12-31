﻿using Nexus.Sdk.Shared.Responses;

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
    /// Update Mail status to Sent
    /// </summary>
    /// <param name="code">Unique identifier of mail</param>
    /// <returns>
    /// Updated Mail Response
    /// </returns>
    public Task<MailsResponse> UpdateMailSent(string code);
}
