namespace Nexus.Sdk.Token.Facades
{
    public class TokenServerFacade
    {
        protected readonly ITokenServerProvider _provider;

        public TokenServerFacade(ITokenServerProvider provider)
        {
            _provider = provider;
        }
    }
}
