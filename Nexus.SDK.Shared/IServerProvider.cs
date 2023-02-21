using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared
{
    public interface IServerProvider
    {
        public Task<CustomerResponse> GetCustomer(string customerCode);
        public Task<CustomerDataResponse> GetCustomerData(string customerCode);
        public Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, string? customerIPAddress = null);
        public Task<bool> Exists(string customerCode);
        public Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? query);
        public Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request, string? customerIPAddress = null);
    }
}
