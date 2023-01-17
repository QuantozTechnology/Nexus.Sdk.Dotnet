using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

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

    public async Task<CustomerResponse> Create(CustomerRequest request)
    {
        return await _provider.CreateCustomer(request);
    }

    /// <summary>
    /// Update customer properties based on the code
    /// </summary>
    /// <returns>
    /// Updated Customer properties
    /// </returns>
    public async Task<CustomerResponse> UpdateCustomer(CustomerRequest request)
    {
        return await _provider.UpdateCustomer(request);
    }
}
