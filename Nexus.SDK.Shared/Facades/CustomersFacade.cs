using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared.Facades;

public class CustomersFacade : ServerFacade, ICustomersFacade
{
    public CustomersFacade(IServerProvider provider) : base(provider)
    {
    }

    public async Task<CustomerResponse> Get(string customerCode)
    {
        return await _provider.GetCustomer(customerCode);
    }

    /// <summary>
    /// Get customer personal data based on the code
    /// </summary>
    /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
    /// <returns>
    /// Customer personal data
    /// </returns>
    public async Task<CustomerDataResponse> GetData(string customerCode)
    {
        return await _provider.GetCustomerData(customerCode);
    }

    public async Task<bool> Exists(string customerCode)
    {
        return await _provider.Exists(customerCode);
    }

    public async Task<CustomerResponse> Create(CreateCustomerRequest request, string? customerIPAddress = null)
    {
        return await _provider.CreateCustomer(request, customerIPAddress);
    }

    /// <summary>
    /// Update customer properties based on the code
    /// </summary>
    /// <returns>
    /// Updated Customer properties
    /// </returns>
    public async Task<CustomerResponse> Update(UpdateCustomerRequest request, string? customerIPAddress = null)
    {
        return await _provider.UpdateCustomer(request, customerIPAddress);
    }
}
