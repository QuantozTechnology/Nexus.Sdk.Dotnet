using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades
{
    public class TokenLimitsFacade : TokenServerFacade
    {
        public TokenLimitsFacade(ITokenServerProvider provider) : base(provider)
        {
        }

        /// <summary>
        /// Get token funding limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <returns>
        /// The current spending limits expressed in token value.
        /// </returns>
        public async Task<TokenLimitsResponse> GetFundingLimits(string customerCode, string tokenCode)
        {
            return await _provider.GetTokenFundingLimits(customerCode, tokenCode);
        }

        /// <summary>
        /// Get token payout limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <returns>
        /// The current fiat spending limits expressed in token value.
        /// </returns>
        public async Task<TokenLimitsResponse> GetPayoutLimits(string customerCode, string tokenCode)
        {
            return await _provider.GetTokenPayoutLimits(customerCode, tokenCode);
        }
    }
}
