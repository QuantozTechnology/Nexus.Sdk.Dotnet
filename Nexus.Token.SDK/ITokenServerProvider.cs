using Nexus.SDK.Shared;
using Nexus.SDK.Shared.Responses;
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
        Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey);

        Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, string[] tokenCodes);

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey);

        Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, string[] tokenCodes);

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


        Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? query);

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
        Task<SignableResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null);

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
        Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests);

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="schema"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<TaxonomySchemaResponse> CreateTaxonomySchema(string code, string schema, string? name = null, string? description = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <returns></returns>
        Task<TaxonomySchemaResponse> GetTaxonomySchema(string taxonomySchemaCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        Task<TaxonomySchemaResponse> UpdateTaxonomySchema(string taxonomySchemaCode, string? name = null, string? description = null, string? schema = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task<TaxonomyResponse> GetTaxonomy(string tokenCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        Task<OrderResponse> GetOrder(string orderCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PagedResponse<OrderResponse>> GetOrders(IDictionary<string, string>? query);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        Task<CancelOrderResponse> CancelOrder(string orderCode);
    }
}
