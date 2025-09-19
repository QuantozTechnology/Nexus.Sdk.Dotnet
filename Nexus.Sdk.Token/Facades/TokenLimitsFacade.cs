using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public class TokenLimitsFacade : TokenServerFacade, ITokenLimitsFacade
    {
        public TokenLimitsFacade(ITokenServerProvider provider) : base(provider)
        {
        }

        public async Task<TokenLimitsResponse> GetFundingLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            return await _provider.GetTokenFundingLimits(customerCode, tokenCode, blockchainCode);
        }

        public async Task<TokenLimitsResponse> GetPayoutLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            return await _provider.GetTokenPayoutLimits(customerCode, tokenCode, blockchainCode);
        }
    }
}
