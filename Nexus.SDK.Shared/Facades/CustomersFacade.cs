using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

public class CustomersFacade : ServerFacade
{
    public CustomersFacade(IServerProvider provider) : base(provider)
    {
    }

    public async Task<CustomerResponse> Get(string customerCode)
    {
        return await _provider.GetCustomer(customerCode);
    }

    public async Task<bool> Exists(string customerCode)
    {
        return await _provider.Exists(customerCode);
    }

    public async Task<CustomerResponse> Create(CustomerRequest request)
    {
        return await _provider.CreateCustomer(request);
    }
}
