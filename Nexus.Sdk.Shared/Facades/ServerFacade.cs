using Nexus.Sdk.Shared;

namespace Nexus.Sdk.Shared.Facades
{
    public class ServerFacade
    {
        protected readonly IServerProvider _provider;

        public ServerFacade(IServerProvider provider)
        {
            _provider = provider;
        }
    }
}
