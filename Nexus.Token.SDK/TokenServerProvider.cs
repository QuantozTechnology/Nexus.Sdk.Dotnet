using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.ErrorHandling;
using Nexus.SDK.Shared.Http;
using Nexus.SDK.Shared.Options;
using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK
{
    public class TokenServerProvider : ITokenServerProvider
    {
        private readonly NexusOptions _options;
        private readonly HttpClient _client;
        private readonly NexusResponseHandler _handler;
        private readonly ILogger? _logger;

        public TokenServerProvider(IHttpClientFactory factory, NexusOptions options, ILogger? logger = null)
        {
            _client = factory.CreateClient("NexusApi");
            _handler = new NexusResponseHandler(logger);
            _options = options;
            _logger = logger;
        }

        public async Task<SignableResponse> CancelOrder(string orderCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "orders", "cancel");
            var request = new CancelOrderRequest(orderCode);
            return await builder.ExecutePut<CancelOrderRequest, SignableResponse>(request);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts", accountCode);

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

            return await builder.ExecutePut<UpdateTokenAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public async Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey
            };

            return await builder.ExecutePost<CreateAlgorandAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            return await builder.ExecutePost<CreateAlgorandAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public async Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey
            };

            return await builder.ExecutePost<CreateStellarAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            return await builder.ExecutePost<CreateStellarAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="trustLevel"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer");
            return await builder.ExecutePost<CustomerRequest, CustomerResponse>(request);
        }

        /// <summary>
        /// Update customer properties based on the code
        /// </summary>
        /// <returns>
        /// Updated Customer properties
        /// </returns>
        public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", request.CustomerCode!);
            return await builder.ExecutePut<CustomerRequest, CustomerResponse>(request);
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
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(_options.PaymentMethodOptions.Funding))
            {
                throw new InvalidOperationException("Funding payment method is required to fund an account with tokens");
            }

            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "fund");

            var account = await GetAccount(accountCode);

            var request = new FundingOperationRequest
            {
                CustomerCode = account.CustomerCode,
                AccountCode = accountCode,
                Definitions = definitions,
                Memo = memo,
                PaymentMethodCode = (pm ?? _options.PaymentMethodOptions.Funding)!
            };

            await builder.ExecutePost(request);
        }

        public async Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "orders");
            return await builder.ExecutePost<OrderRequest, CreateOrderResponse>(orderRequest);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payments");

            var request = new PaymentOperationRequest(definitions, memo);
            return await builder.ExecutePost<PaymentOperationRequest, SignableResponse>(request);
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
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(_options.PaymentMethodOptions.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payouts");

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? _options.PaymentMethodOptions.Payout,
                Amount = amount,
                TokenCode = tokenCode,
                Memo = memo
            };

            return await builder.ExecutePost<PayoutOperationRequest, SignableResponse>(request);
        }

        public async Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(_options.PaymentMethodOptions.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payouts", "simulate");

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? _options.PaymentMethodOptions.Payout,
                Amount = amount,
                TokenCode = tokenCode,
                Memo = memo
            };

            return await builder.ExecutePost<PayoutOperationRequest, PayoutOperationResponse>(request);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("taxonomy", "schema");

            var request = new CreateTaxonomySchemaRequest(code, schema)
            {
                Description = description,
                Name = name
            };

            return await builder.ExecutePost<CreateTaxonomySchemaRequest, TaxonomySchemaResponse>(request);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens");

            var request = new AlgorandTokenRequest
            {
                AlgorandTokens = new AlgorandTokens
                {
                    Definitions = definitions,
                    AlgorandSettings = settings ?? new AlgorandTokenSettings()
                }
            };

            return await builder.ExecutePost<AlgorandTokenRequest, CreateTokenResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens");

            var request = new StellarTokenRequest
            {
                StellarTokens = new StellarTokens
                {
                    Definitions = definitions,
                    StellarSettings = settings ?? new StellarTokenSettings()
                }
            };

            return await builder.ExecutePost<StellarTokenRequest, CreateTokenResponse>(request);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts", accountCode);
            return await builder.ExecuteGet<AccountResponse>();
        }

        /// <summary>
        /// Lists token accounts based on the query parameters
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of token accounts
        /// </returns>
        public async Task<PagedResponse<AccountResponse>> GetAccounts(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<AccountResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
        public async Task<AccountBalancesResponse> GetAccountBalanceAsync(string accountCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts", accountCode, "tokenBalance");
            return await builder.ExecuteGet<AccountBalancesResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> GetCustomer(string customerCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode);
            return await builder.ExecuteGet<CustomerResponse>();
        }

        /// <summary>
        /// Get customer personal data based on the code
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <returns>
        /// Customer personal data
        /// </returns>
        public async Task<CustomerDataResponse> GetCustomerData(string customerCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "personalData");
            return await builder.ExecuteGet<CustomerDataResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public async Task<OrderResponse> GetOrder(string orderCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "orders", orderCode);
            return await builder.ExecuteGet<OrderResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<OrderResponse>> GetOrders(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "orders");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<OrderResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TaxonomyResponse> GetTaxonomy(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("taxonomy", "token", tokenCode);
            return await builder.ExecuteGet<TaxonomyResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <returns></returns>
        public async Task<TaxonomySchemaResponse> GetTaxonomySchema(string taxonomySchemaCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("taxonomy", "schema", taxonomySchemaCode);
            return await builder.ExecuteGet<TaxonomySchemaResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetToken(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens", tokenCode);
            return await builder.ExecuteGet<TokenResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<TokenResponse>>();
        }

        /// <summary>
        /// Get token operation details based on the code
        /// </summary>
        /// <param name="code">Unique Nexus identifier of the operation.</param>
        /// <returns>
        /// Return token operation details
        /// </returns>
        public async Task<TokenOperationResponse> GetTokenPayment(string code)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payments", code);
            return await builder.ExecuteGet<TokenOperationResponse>();
        }

        /// <summary>
        /// Lists token operations based on the query parameters
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of token payments, fundings, payouts and clawbacks
        /// </returns>
        public async Task<PagedResponse<TokenOperationResponse>> GetTokenPayments(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payments");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<TokenOperationResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "envelope", "signature", "submit");

            foreach (var request in requests)
            {
                await builder.ExecutePost(request);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnStellarAsync(StellarSubmitRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "envelope", "submit");
            await builder.ExecutePost(request);
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
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("taxonomy", "schema", taxonomySchemaCode);

            var request = new UpdateTaxonomySchemaRequest
            {
                Name = name,
                Description = description,
                Schema = schema
            };

            return await builder.ExecutePut<UpdateTaxonomySchemaRequest, TaxonomySchemaResponse>(request);
        }

        public async Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "limits", "tokenfunding", "token", tokenCode);
            return await builder.ExecuteGet<TokenLimitsResponse>();
        }

        public async Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "limits", "tokenpayout", "token", tokenCode);
            return await builder.ExecuteGet<TokenLimitsResponse>();
        }

        public async Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("labelpartner", "trustlevels");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<TrustLevelsResponse>>();
        }
    }
}
