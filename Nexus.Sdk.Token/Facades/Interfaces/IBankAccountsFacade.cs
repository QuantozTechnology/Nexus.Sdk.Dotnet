using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades.Interfaces;

public interface IBankAccountsFacade
{
    /// <summary>
    /// List bank accounts based on the query parameters
    /// </summary>
    /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Return a paged list of bank accounts
    /// </returns>
    public Task<PagedResponse<BankAccountResponse>> Get(IDictionary<string, string> query);

    /// <summary>
    /// Update bank account
    /// </summary>
    /// <param name="updateRequest"></param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns></returns>
    public Task<BankAccountResponse> Update(UpdateBankAccountRequest updateRequest, string? customerIPAddress = null);

    /// <summary>
    /// Create bank account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
    /// <returns></returns>
    public Task<BankAccountResponse> Create(CreateBankAccountRequest request, string? customerIPAddress = null);

    /// <summary>
    /// Delete bank account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task Delete(DeleteBankAccountRequest request);
}
