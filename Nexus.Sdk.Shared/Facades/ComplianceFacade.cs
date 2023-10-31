using Nexus.Sdk.Shared.Facades.Interfaces;

namespace Nexus.Sdk.Shared.Facades
{
    public class ComplianceFacade : IComplianceFacade
    {
        private IServerProvider _serverProvider;

        public ComplianceFacade(IServerProvider serverProvider)
        {
            _serverProvider = serverProvider;
        }

        public ITrustLevelsFacade Trustlevels => new TrustLevelsFacade(_serverProvider);

        public IMailsFacade Mails => new MailsFacade(_serverProvider);
    }
}
