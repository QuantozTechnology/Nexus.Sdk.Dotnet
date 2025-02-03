using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;

namespace Nexus.Sdk.Shared
{
    public interface IServerProvider
    {
        Task<CustomerResponse> GetCustomer(string customerCode);
        Task<PagedResponse<CustomerResponse>> GetCustomers(IDictionary<string, string>? queryParameters);
        Task<CustomerDataResponse> GetCustomerData(string customerCode);
        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, string? customerIPAddress = null);
        Task<bool> Exists(string customerCode);
        Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? query);
        Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? query);
        Task<PagedResponse<MailsResponse>> GetMails(IDictionary<string, string>? query);
        Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request, string? customerIPAddress = null);
        Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, string? customerIPAddress = null);
        Task<MailsResponse> UpdateMailSent(string code);
        Task<MailsResponse> CreateMail(CreateMailRequest request);
        Task<PaymentMethodsResponse> GetPaymentMethod(string paymentMethodCode);
        Task<PagedResponse<CustomerTraceResponse>> GetCustomerTrace(string customerCode, IDictionary<string, string>? queryParameters);

    }
}
