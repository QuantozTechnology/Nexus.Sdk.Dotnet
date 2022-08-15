using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared
{
    public interface IServerProvider
    {
        public Task<CustomerResponse> GetCustomer(string customerCode);
        public Task<CreateCustomerResponse> CreateCustomer(string code, string trustLevel, string currency);
        public Task<bool> Exists(string customerCode);
    }
}
