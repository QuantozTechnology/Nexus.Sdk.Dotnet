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
        private readonly string? FundingPaymentMethod;
        private readonly string? PayoutPaymentMethod;


        public TokenServerProvider(HttpClient httpClient, TokenServerProviderOptions options,
            ILogger<RequestBuilder<TokenServerProvider>>? logger = null, ILogger<AuthProvider>? authLogger = null)
            : base(options.ApiUrl,
                  httpClient,
                  new AuthProvider(options.AuthProviderOptions, authLogger),
                  logger)
        {
            AddDefaultRequestHeader("api_version", "1.2");
            FundingPaymentMethod = options.PaymentMethodOptions.Funding;
            PayoutPaymentMethod = options.PaymentMethodOptions.Payout;
        }

        public async Task<SignableResponse> CancelOrder(string orderCode)
        {
            SetSegments("token", "orders", "cancel");

            var request = new CancelOrderRequest(orderCode);
            return await ExecutePut<CancelOrderRequest, SignableResponse>(request);
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
        public async Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, IEnumerable<string> tokenCodes)
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
        public async Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey
            };

            return await ExecutePost<CreateAlgorandAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            return await ExecutePost<CreateAlgorandAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public async Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey
            };

            return await ExecutePost<CreateStellarAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
        {
            SetSegments("customer", customerCode, "accounts");

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            return await ExecutePost<CreateStellarAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="trustLevel"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            SetSegments("customer");
            return await ExecutePost<CustomerRequest, CustomerResponse>(request);
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
        public async Task CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(FundingPaymentMethod))
            {
                throw new InvalidOperationException("Funding payment method is required to fund an account with tokens");
            }

            SetSegments("token", "fund");

            var request = new FundingOperationRequest
            {
                AccountCode = accountCode,
                Definitions = definitions,
                Memo = memo,
                PaymentMethodCode = pm ?? FundingPaymentMethod
            };

            await ExecutePost(request);
        }

        public async Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            SetSegments("token", "orders");
            return await ExecutePost<OrderRequest, CreateOrderResponse>(orderRequest);
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
            return await CreatePaymentsAsync(new PaymentDefinition[] { definition }, memo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public async Task<SignableResponse> CreatePaymentsAsync(IEnumerable<PaymentDefinition> definitions, string? memo = null)
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
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(PayoutPaymentMethod))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            SetSegments("token", "payouts");

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? PayoutPaymentMethod,
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
        public async Task<TaxonomySchemaResponse> CreateTaxonomySchema(string code, string schema, string? name = null, string? description = null)
        {
            SetSegments("taxonomy", "schema");

            var request = new CreateTaxonomySchemaRequest(code, schema)
            {
                Description = description,
                Name = name
            };

            return await ExecutePost<CreateTaxonomySchemaRequest, TaxonomySchemaResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null)
        {
            return await CreateTokensOnAlgorand(new AlgorandTokenDefinition[] { definition }, settings);
        }

        public async Task<CreateTokenResponse> CreateTokensOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null)
        {
            SetSegments("token", "tokens");

            var request = new AlgorandTokenRequest
            {
                AlgorandTokens = new AlgorandTokens
                {
                    Definitions = definitions,
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
        public async Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null)
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
            return await CreateTokensOnStellarAsync(new StellarTokenDefinition[] { definition }, settings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>True if the customer already exists in Nexus and false otherwise</returns>
        public async Task<bool> Exists(string customerCode)
        {
            try
            {
                await GetCustomer(customerCode);
                return true;
            }
            catch (NexusApiException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return false;
                }

                throw ex;
            }
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
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
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public async Task<OrderResponse> GetOrder(string orderCode)
        {
            SetSegments("token", "orders", orderCode);
            return await ExecuteGet<OrderResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<OrderResponse>> GetOrders(IDictionary<string, string>? queryParameters)
        {
            SetSegments("token", "orders");

            if (queryParameters != null)
            {
                SetQueryParameters(queryParameters);
            }

            return await ExecuteGet<PagedResponse<OrderResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TaxonomyResponse> GetTaxonomy(string tokenCode)
        {
            SetSegments("taxonomy", "token", tokenCode);
            return await ExecuteGet<TaxonomyResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <returns></returns>
        public async Task<TaxonomySchemaResponse> GetTaxonomySchema(string taxonomySchemaCode)
        {
            SetSegments("taxonomy", "schema", taxonomySchemaCode);
            return await ExecuteGet<TaxonomySchemaResponse>();
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
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? queryParameters)
        {
            SetSegments("token", "tokens");

            if (queryParameters != null)
            {
                SetQueryParameters(queryParameters);
            }

            return await ExecuteGet<PagedResponse<TokenResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> GetTokenPayment(string code)
        {
            SetSegments("token", "payments", code);
            return await ExecuteGet<TransactionResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<TransactionResponse>> GetTokenPayments(IDictionary<string, string>? queryParameters)
        {
            SetSegments("token", "payments");

            if (queryParameters != null)
            {
                SetQueryParameters(queryParameters);
            }

            return await ExecuteGet<PagedResponse<TransactionResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests)
        {
            foreach (var request in requests)
            {
                SetSegments("token", "envelope", "signature", "submit");
                await ExecutePost(request);
            }
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
        public async Task<TaxonomySchemaResponse> UpdateTaxonomySchema(string taxonomySchemaCode, string? name = null,
            string? description = null, string? schema = null)
        {
            SetSegments("taxonomy", "schema", taxonomySchemaCode);

            var request = new UpdateTaxonomySchemaRequest
            {
                Name = name,
                Description = description,
                Schema = schema
            };

            return await ExecutePut<UpdateTaxonomySchemaRequest, TaxonomySchemaResponse>(request);
        }
    }
}
