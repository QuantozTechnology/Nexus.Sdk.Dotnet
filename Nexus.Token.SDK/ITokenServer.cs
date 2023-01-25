using Nexus.SDK.Shared.Facades;
using Nexus.SDK.Shared.Facades.Interfaces;
using Nexus.Token.SDK.Facades;

namespace Nexus.Token.SDK
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
