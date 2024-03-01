using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public interface ICustomersFacade
{
    /// <summary>
    /// Get a customer
    /// </summary>
    /// <param name="customerCode">Code of the customer</param>
    /// <returns>Nexus customer matching the provided code</returns>
    public Task<CustomerResponse> Get(string customerCode);

    /// <summary>
    /// Gets a list of customers based on query parameters
    /// </summary>
    /// <returns>Nexus customers matching the provided query parameters</returns>
    public Task<PagedResponse<CustomerResponse>> Get(IDictionary<string, string>? query);

    /// <summary>
    /// Check if a customer exists
    /// </summary>
    /// <param name="customerCode">Code of the customer</param>
    /// <returns>True if there exists a Nexus customer matching the provided code</returns>
    public Task<bool> Exists(string customerCode);

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="request">Properties the customer is created with</param>
    /// <returns>The Nexus customer that is created</returns>
    public Task<CustomerResponse> Create(CreateCustomerRequest request, string? customerIPAddress = null);

    /// <summary>
    /// Update customer properties based on the code
    /// </summary>
    /// <returns>
    /// Updated Customer properties
    /// </returns>
    public Task<CustomerResponse> Update(UpdateCustomerRequest request, string? customerIPAddress = null);

    /// <summary>
    /// Get customer personal data based on the code
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <returns>
    /// Customer personal data
    /// </returns>
    public Task<CustomerDataResponse> GetData(string customerCode);
}
