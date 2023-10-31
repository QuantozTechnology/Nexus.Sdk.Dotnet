namespace Nexus.Sdk.Shared.Facades.Interfaces
{
    public interface IComplianceFacade
    {
        ITrustLevelsFacade Trustlevels { get; }

        IMailsFacade Mails { get; }
    }
}
