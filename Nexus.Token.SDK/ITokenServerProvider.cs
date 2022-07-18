using Nexus.SDK.Shared;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK
{
    public interface ITokenServerProvider : IServerProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
        Task<AccountResponse> GetAccount(string accountCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        Task<CreateAccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey);

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        Task<CreateAccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
        Task<AccountBalancesResponse> GetAccountBalanceAsync(string accountCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCodes"></param>
        /// <returns></returns>
        Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, string[] tokenCodes);

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task<TokenResponse> GetToken(string tokenCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition[] definitions, StellarTokenSettings? settings = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="definitions"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        Task CreateFundingAsync(string accountCode, FundingDefinition[] definitions, string? pm = null, string? memo = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="senderPublicKey"></param>
        /// <param name="receiverPublicKey"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        Task<SignableResponse> CreatePaymentAsync(PaymentDefinition[] definitions, string? memo = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SubmitOnStellarAsync(StellarSubmitRequest request);

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SubmitOnAlgorandAsync(AlgorandSubmitRequest request);
    }
}
