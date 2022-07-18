namespace Nexus.Token.SDK.Facades
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
