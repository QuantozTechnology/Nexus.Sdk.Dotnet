using Nexus.SDK.Shared.Facades;
using Nexus.Token.SDK.Facades;

namespace Nexus.Token.SDK
{
    public interface ITokenServer
    {
        public CustomersFacade Customers { get; }
        public AccountsFacade Accounts { get; }
        public TokensFacade Tokens { get; }
        public OperationsFacade Operations { get; }
        public SubmitFacade Submit { get; }
    }
}
