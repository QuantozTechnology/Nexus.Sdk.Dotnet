using Nexus.Sdk.Shared.Facades;
using Nexus.Sdk.Shared.Facades.Interfaces;
using Nexus.Sdk.Token.Facades;
using Nexus.Sdk.Token.Facades.Interfaces;

namespace Nexus.Sdk.Token
{
    public interface ITokenServer
    {
        public ICustomersFacade Customers { get; }
        public IAccountsFacade Accounts { get; }
        public ITokensFacade Tokens { get; }
        public IOperationsFacade Operations { get; }
        public ISubmitFacade Submit { get; }
        public ITaxonomyFacade Taxonomy { get; }
        public IOrdersFacade Orders { get; }
        public ITokenLimitsFacade TokenLimits { get; }
        public IComplianceFacade Compliance { get; }
        public IPaymentMethodsFacade PaymentMethods { get; }
        public ILabelPartnerFacade LabelPartner { get; }
        public IBankAccountsFacade BankAccounts { get; }
        public IDocumentStoreFacade DocumentStore { get; }

        /// <summary>
        /// Execute a GET request against a custom API path.
        /// </summary>
        Task<TResponse> Get<TResponse>(string path, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null) where TResponse : class;

        /// <summary>
        /// Execute a POST request against a custom API path.
        /// </summary>
        Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null) where TRequest : class where TResponse : class;

        /// <summary>
        /// Execute a PUT request against a custom API path.
        /// </summary>
        Task<TResponse> Put<TRequest, TResponse>(string path, TRequest request, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null) where TRequest : class where TResponse : class;

        /// <summary>
        /// Execute a DELETE request against a custom API path.
        /// </summary>
        Task<TResponse> Delete<TResponse>(string path, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null) where TResponse : class;
    }
}
