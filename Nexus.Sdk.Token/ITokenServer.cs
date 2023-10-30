using Nexus.Sdk.Shared.Facades;
using Nexus.Sdk.Shared.Facades.Interfaces;
using Nexus.Sdk.Token.Facades;

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
    }
}
