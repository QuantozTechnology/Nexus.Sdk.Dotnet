using Nexus.SDK.Shared.Facades;
using Nexus.Token.SDK.Facades;

namespace Nexus.Token.SDK;

public class TokenServer : ITokenServer
{
    private ITokenServerProvider _provider;

    public TokenServer(ITokenServerProvider provider)
    {
        _provider = provider;
    }

    public ICustomersFacade Customers => new CustomersFacade(_provider);
    public IAccountsFacade Accounts => new AccountsFacade(_provider);
    public ITokensFacade Tokens => new TokensFacade(_provider);
    public IOperationsFacade Operations => new OperationsFacade(_provider);
    public ISubmitFacade Submit => new SubmitFacade(_provider);
    public ITaxonomyFacade Taxonomy => new TaxonomyFacade(_provider);
    public IOrdersFacade Orders => new OrdersFacade(_provider);
    public ITokenLimitsFacade TokenLimits => new TokenLimitsFacade(_provider);
    public ITrustLevelsFacade TrustLevels => new TrustLevelsFacade(_provider);
}