using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared
{
    public interface IServerProvider
    {
        public Task<CustomerResponse> GetCustomer(string customerCode);
        public Task<CustomerResponse> CreateCustomer(CustomerRequest request);
        public Task<bool> Exists(string customerCode);
    }
}
