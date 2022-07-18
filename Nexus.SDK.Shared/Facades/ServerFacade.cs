using Nexus.SDK.Shared;

namespace Nexus.SDK.Shared.Facades
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
