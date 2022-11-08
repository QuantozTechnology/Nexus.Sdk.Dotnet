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

    public CustomersFacade Customers => new(_provider);
    public AccountsFacade Accounts => new(_provider);
    public TokensFacade Tokens => new(_provider);
    public OperationsFacade Operations => new(_provider);
    public SubmitFacade Submit => new(_provider);
    public TaxonomyFacade Taxonomy => new(_provider);
    public OrdersFacade Orders => new(_provider);
    public TransactionsFacade Transactions => new(_provider);
}