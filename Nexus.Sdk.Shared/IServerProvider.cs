using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared
{
    public interface IServerProvider
    {
        public Task<CustomerResponse> GetCustomer(string customerCode);
        public Task<PagedResponse<CustomerResponse>> GetCustomers(IDictionary<string, string>? queryParameters);
        public Task<CustomerDataResponse> GetCustomerData(string customerCode);
        public Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, string? customerIPAddress = null);
        public Task<bool> Exists(string customerCode);
        public Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? query);
        public Task<PagedResponse<MailsResponse>> GetMails(IDictionary<string, string>? query);
        public Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request, string? customerIPAddress = null);
        public Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, string? customerIPAddress = null);
        public Task<MailsResponse> UpdateMailSent(string code);
    }
}
