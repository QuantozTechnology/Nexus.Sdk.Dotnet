using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades
{
    public class TokenLimitsFacade : TokenServerFacade, ITokenLimitsFacade
    {
        public TokenLimitsFacade(ITokenServerProvider provider) : base(provider)
        {
        }

        public async Task<TokenLimitsResponse> GetFundingLimits(string customerCode, string tokenCode)
        {
            return await _provider.GetTokenFundingLimits(customerCode, tokenCode);
        }

        public async Task<TokenLimitsResponse> GetPayoutLimits(string customerCode, string tokenCode)
        {
            return await _provider.GetTokenPayoutLimits(customerCode, tokenCode);
        }
    }
}
