using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK
{
    public class TokenServerProvider : RequestBuilder<TokenServerProvider>, ITokenServerProvider
    {
        private readonly IDictionary<PaymentMethodType, string> _paymentMethods;

        public TokenServerProvider(HttpClient httpClient, IAuthProvider authProvider,
            TokenServerProviderOptions options, ILogger<RequestBuilder<TokenServerProvider>>? logger = null)
            : base(options.ServerUri, httpClient, authProvider, logger)
        {
            _paymentMethods = options.PaymentMethods ?? new Dictionary<PaymentMethodType, string>();
            httpClient.DefaultRequestHeaders.Add("api_version", "1.2");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode)
        {
            return await ConnectAccountToTokensAsync(accountCode, new string[] { tokenCode });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCodes"></param>
        /// <returns></returns>
        public async Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, string[] tokenCodes)
        {
            SetSegments("accounts", accountCode);

            var request = new UpdateTokenAccountRequest
            {
                Settings = new UpdateTokenAccountSettings
                {
                    AllowedTokens = new AllowedTokens
                    {
                        AddTokens = tokenCodes
                    }
                }
            };

            return await ExecutePut<UpdateTokenAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public async Task<CreateAccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey
            };

            return await ExecutePost<CreateAlgorandAccountRequest, CreateAccountResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public async Task<CreateAccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey
            };

            return await ExecutePost<CreateStellarAccountRequest, CreateAccountResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="trustLevel"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<CreateCustomerResponse> CreateCustomer(string code, string trustLevel, string currency)
        {
            SetSegments("customer");
            var request = new CustomerRequest(code, trustLevel, currency);
            return await ExecutePost<CustomerRequest, CreateCustomerResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public async Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            var definition = new FundingDefinition(tokenCode, amount);
            await CreateFundingAsync(accountCode, new FundingDefinition[] { definition }, pm, memo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="definitions"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CreateFundingAsync(string accountCode, FundingDefinition[] definitions, string? pm = null, string? memo = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && !_paymentMethods.ContainsKey(PaymentMethodType.Funding))
            {
                throw new InvalidOperationException("Funding payment method is required to fund an account with tokens");
            }

            SetSegments("token", "fund");

            var request = new FundingOperationRequest
            {
                AccountCode = accountCode,
                Definitions = definitions,
                Memo = memo,
                PaymentMethodCode = pm ?? _paymentMethods[PaymentMethodType.Funding]
            };

            await ExecutePost(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="senderPublicKey"></param>
        /// <param name="receiverPublicKey"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public async Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null)
        {
            var definition = new PaymentDefinition(senderPublicKey, receiverPublicKey, tokenCode, amount);
            return await CreatePaymentAsync(new PaymentDefinition[] { definition }, memo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public async Task<SignableResponse> CreatePaymentAsync(PaymentDefinition[] definitions, string? memo = null)
        {
            SetSegments("token", "payments");

            var request = new PaymentOperationRequest(definitions, memo);
            return await ExecutePost<PaymentOperationRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && !_paymentMethods.ContainsKey(PaymentMethodType.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            SetSegments("token", "payouts");

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? _paymentMethods[PaymentMethodType.Payout],
                Amount = amount,
                TokenCode = tokenCode,
                Memo = memo
            };

            return await ExecutePost<PayoutOperationRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetTaxonomySchemaResponse> CreateTaxonomySchema(string code, string schema, string? name = null, string? description = null)
        {
            SetSegments("taxonomy", "schema");

            var request = new CreateTaxonomySchemaRequest(code, schema)
            {
                Description = description,
                Name = name
            };

            return await ExecutePost<CreateTaxonomySchemaRequest, GetTaxonomySchemaResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null)
        {
            SetSegments("token", "tokens");

            var request = new AlgorandTokenRequest
            {
                AlgorandTokens = new AlgorandTokens
                {
                    Definition = definition,
                    AlgorandSettings = settings ?? new AlgorandTokenSettings()
                }
            };

            return await ExecutePost<AlgorandTokenRequest, CreateTokenResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition[] definitions, StellarTokenSettings? settings = null)
        {
            SetSegments("token", "tokens");

            var request = new StellarTokenRequest
            {
                StellarTokens = new StellarTokens
                {
                    Definitions = definitions,
                    StellarSettings = settings ?? new StellarTokenSettings()
                }
            };

            return await ExecutePost<StellarTokenRequest, CreateTokenResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null)
        {
            return await CreateTokenOnStellarAsync(new StellarTokenDefinition[] { definition }, settings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
        public async Task<AccountResponse> GetAccount(string accountCode)
        {
            SetSegments("accounts", accountCode);
            return await ExecuteGet<AccountResponse>();
        }

        public async Task<AccountBalancesResponse> GetAccountBalanceAsync(string accountCode)
        {
            SetSegments("accounts", accountCode, "tokenBalance");
            return await ExecuteGet<AccountBalancesResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> GetCustomer(string customerCode)
        {
            SetSegments("customer", customerCode);
            return await ExecuteGet<CustomerResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GetTaxonomyResponse> GetTaxonomy(string tokenCode)
        {
            SetSegments("taxonomy", "token", tokenCode);
            return await ExecuteGet<GetTaxonomyResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <returns></returns>
        public async Task<GetTaxonomySchemaResponse> GetTaxonomySchema(string taxonomySchemaCode)
        {
            SetSegments("taxonomy", "schema", taxonomySchemaCode);
            return await ExecuteGet<GetTaxonomySchemaResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetToken(string tokenCode)
        {
            SetSegments("token", "tokens", tokenCode);
            return await ExecuteGet<TokenResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnAlgorandAsync(AlgorandSubmitRequest request)
        {
            SetSegments("token", "envelope", "signature", "submit");
            await ExecutePost(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnStellarAsync(StellarSubmitRequest request)
        {
            SetSegments("token", "envelope", "submit");
            await ExecutePost(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public async Task<GetTaxonomySchemaResponse> UpdateTaxonomySchema(string taxonomySchemaCode, string? name = null,
            string? description = null, string? schema = null)
        {
            SetSegments("taxonomy", "schema", taxonomySchemaCode);

            var request = new UpdateTaxonomySchemaRequest
            {
                Name = name,
                Description = description,
                Schema = schema
            };

            return await ExecutePut<UpdateTaxonomySchemaRequest, GetTaxonomySchemaResponse>(request);
        }
    }
}
