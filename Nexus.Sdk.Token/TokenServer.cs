using Nexus.Sdk.Shared.Facades;
using Nexus.Sdk.Shared.Facades.Interfaces;
using Nexus.Sdk.Token.Facades;
using Nexus.Sdk.Token.Facades.Interfaces;

namespace Nexus.Sdk.Token;

public class TokenServer : ITokenServer
{
    private ITokenServerProvider _serverProvider;

    public TokenServer(ITokenServerProvider provider)
    {
        _serverProvider = provider;
    }

    public ICustomersFacade Customers => new CustomersFacade(_serverProvider);
    public IAccountsFacade Accounts => new AccountsFacade(_serverProvider);
    public ITokensFacade Tokens => new TokensFacade(_serverProvider);
    public IOperationsFacade Operations => new OperationsFacade(_serverProvider);
    public ISubmitFacade Submit => new SubmitFacade(_serverProvider);
    public ITaxonomyFacade Taxonomy => new TaxonomyFacade(_serverProvider);
    public IOrdersFacade Orders => new OrdersFacade(_serverProvider);
    public ITokenLimitsFacade TokenLimits => new TokenLimitsFacade(_serverProvider);
    public IComplianceFacade Compliance => new ComplianceFacade(_serverProvider);
    public IPaymentMethodsFacade PaymentMethods => new PaymentMethodsFacade(_serverProvider);
    public ILabelPartnerFacade LabelPartner => new LabelPartnerFacade(_serverProvider);
    public IBankAccountsFacade BankAccounts => new BankAccountsFacade(_serverProvider);
    public IDocumentStoreFacade DocumentStore => new DocumentStoreFacade(_serverProvider);
    public IEventsFacade Events => new EventsFacade(_serverProvider);

    /// <summary>
    /// Execute a GET request against a custom API path.
    /// </summary>
    public Task<TResponse> Get<TResponse>(string path, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null)
        where TResponse : class
        => _serverProvider.SendGetRequest<TResponse>(path, queryParameters, headers);

    /// <summary>
    /// Execute a POST request against a custom API path.
    /// </summary>
    public Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null)
        where TRequest : class
        where TResponse : class
        => _serverProvider.SendPostRequest<TRequest, TResponse>(path, request, queryParameters, headers);

    /// <summary>
    /// Execute a PUT request against a custom API path.
    /// </summary>
    public Task<TResponse> Put<TRequest, TResponse>(string path, TRequest request, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null)
        where TRequest : class
        where TResponse : class
        => _serverProvider.SendPutRequest<TRequest, TResponse>(path, request, queryParameters, headers);

    /// <summary>
    /// Execute a DELETE request against a custom API path.
    /// </summary>
    public Task<TResponse> Delete<TResponse>(string path, IDictionary<string, string>? queryParameters = null, IDictionary<string, string>? headers = null)
        where TResponse : class
        => _serverProvider.SendDeleteRequest<TResponse>(path, queryParameters, headers);
}