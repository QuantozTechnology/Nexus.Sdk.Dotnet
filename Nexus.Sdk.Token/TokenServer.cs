using Nexus.Sdk.Shared.Facades;
using Nexus.Sdk.Shared.Facades.Interfaces;
using Nexus.Sdk.Token.Facades;

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
}