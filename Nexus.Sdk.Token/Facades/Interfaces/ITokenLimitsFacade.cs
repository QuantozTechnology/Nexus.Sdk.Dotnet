using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public interface ITokenLimitsFacade
    {
        /// <summary>
        /// Get token funding limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <param name="blockchainCode">Blockchain code (ex. ETH instead of Ethereum) used to further identify the token.</param>
        /// <returns>
        /// The current spending limits expressed in token value.
        /// </returns>
        public Task<TokenLimitsResponse> GetFundingLimits(string customerCode, string tokenCode, string? blockchainCode = null);

        /// <summary>
        /// Get token payout limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <param name="blockchainCode">Blockchain code (ex. ETH instead of Ethereum) used to further identify the token.</param>
        /// <returns>
        /// The current fiat spending limits expressed in token value.
        /// </returns>
        public Task<TokenLimitsResponse> GetPayoutLimits(string customerCode, string tokenCode, string? blockchainCode = null);
    }
}
