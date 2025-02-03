using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.ErrorHandling;
using Nexus.Sdk.Shared.Http;
using Nexus.Sdk.Shared.Options;
using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token
{
    public class TokenServerProvider : ITokenServerProvider
    {
        private readonly NexusOptions _options;
        private readonly HttpClient _client;
        private readonly NexusResponseHandler _handler;
        private readonly ILogger<TokenServerProvider> _logger;

        public TokenServerProvider(IHttpClientFactory factory, NexusOptions options, ILogger<TokenServerProvider> logger)
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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode, string? customerIPAddress = null)
        {
            return await ConnectAccountToTokensAsync(accountCode, [tokenCode], customerIPAddress);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCodes"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, IEnumerable<string> tokenCodes, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts", accountCode);

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
        /// <returns></returns>
        public async Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey,
                AccountType = accountType
            };

            if (!string.IsNullOrWhiteSpace(customName))
            {
                request.CustomName = customName;
            }

            return await builder.ExecutePost<CreateAlgorandAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new CreateAlgorandAccountRequest
            {
                Address = publicKey,
                AccountType = accountType,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            if (!string.IsNullOrWhiteSpace(customName))
            {
                request.CustomName = customName;
            }

            return await builder.ExecutePost<CreateAlgorandAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey,
                AccountType = accountType
            };

            if (!string.IsNullOrWhiteSpace(customName))
            {
                request.CustomName = customName;
            }

            return await builder.ExecutePost<CreateStellarAccountRequest, AccountResponse>(request);
        }

        public async Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null, string ? accountType = "MANAGED")
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new CreateStellarAccountRequest
            {
                Address = publicKey,
                AccountType = accountType,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            if (!string.IsNullOrWhiteSpace(customName))
            {
                request.CustomName = customName;
            }

            return await builder.ExecutePost<CreateStellarAccountRequest, SignableResponse>(request);
        }

        /// <summary>
        /// Update token account settings
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="accountCode"></param>
        /// <param name="updateRequest"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<SignableResponse> UpdateAccount(string customerCode, string accountCode, UpdateTokenAccountRequest updateRequest, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "accounts", accountCode);

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            return await builder.ExecutePut<UpdateTokenAccountRequest, SignableResponse>(updateRequest);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="trustLevel"></param>
        /// <param name="currency"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            return await builder.ExecutePost<CreateCustomerRequest, CustomerResponse>(request);
        }

        /// <summary>
        /// Update customer properties based on the code
        /// </summary>
        /// <returns>
        /// Updated Customer properties
        /// </returns>
        public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", request.CustomerCode!);

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            return await builder.ExecutePut<UpdateCustomerRequest, CustomerResponse>(request);
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        public async Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", request.CustomerCode!);

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            return await builder.ExecuteDelete<DeleteCustomerResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="message"></param>
        /// <param name="paymentReference"></param>
        /// <returns></returns>
        public async Task<FundingResponses> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null)
        {
            var definition = new FundingDefinition(tokenCode, amount, paymentReference);
            return await CreateFundingAsync(accountCode, new FundingDefinition[] { definition }, pm, memo, message, customerIPAddress);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="definitions"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<FundingResponses> CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null, string? message = null, string? customerIPAddress = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(_options.PaymentMethodOptions.Funding))
            {
                throw new InvalidOperationException("Funding payment method is required to fund an account with tokens");
            }

            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "fund");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var account = await GetAccount(accountCode);

            var request = new FundingOperationRequest
            {
                CustomerCode = account.CustomerCode,
                AccountCode = accountCode,
                Definitions = definitions,
                Memo = memo,
                Message = message,
                PaymentMethodCode = (pm ?? _options.PaymentMethodOptions.Funding)!
            };

            return await builder.ExecutePost<FundingOperationRequest, FundingResponses>(request);
        }

        public async Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "orders");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

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
        /// <param name="message"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null)
        {
            var definition = new PaymentDefinition(senderPublicKey, receiverPublicKey, tokenCode, amount);
            return await CreatePaymentsAsync([definition], memo, message, cryptoCode, callbackUrl, customerIPAddress);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="memo"></param>
        /// <param name="message"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<SignablePaymentResponse> CreatePaymentsAsync(IEnumerable<PaymentDefinition> definitions, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payments");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new PaymentOperationRequest(definitions, memo, message, cryptoCode, callbackUrl);
            return await builder.ExecutePost<PaymentOperationRequest, SignablePaymentResponse>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <param name="message"></param>
        /// <param name="paymentReference"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? blockchainTransactionId = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(_options.PaymentMethodOptions.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "payouts");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? _options.PaymentMethodOptions.Payout,
                Amount = amount,
                TokenCode = tokenCode,
                PaymentReference = paymentReference,
                Memo = memo,
                Message = message,
                BlockchainTransactionId = blockchainTransactionId
            };

            return await builder.ExecutePost<PayoutOperationRequest, SignablePayoutResponse>(request);
        }

        public async Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null, string? blockchainTransactionId = null)
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
                PaymentReference = paymentReference,
                Memo = memo,
                BlockchainTransactionId = blockchainTransactionId
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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null, string? customerIPAddress = null)
        {
            return await CreateTokensOnAlgorand([definition], settings, customerIPAddress);
        }

        public async Task<CreateTokenResponse> CreateTokensOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens");

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null, string? customerIPAddress = null)
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

                throw;
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
        /// List Customers based on query paramaters
        /// </summary>
        /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Paged list of customers
        /// </returns>
        public async Task<PagedResponse<CustomerResponse>> GetCustomers(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<CustomerResponse>>();
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
        /// List customer traces based on the code
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Paged list of customer traces
        /// </returns>
        public async Task<PagedResponse<CustomerTraceResponse>> GetCustomerTrace(string customerCode, IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("customer", customerCode, "trace");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }
            return await builder.ExecuteGet<PagedResponse<CustomerTraceResponse>>();
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
        public async Task<TokenDetailsResponse> GetToken(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens", tokenCode);
            return await builder.ExecuteGet<TokenDetailsResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<TokenBalancesResponse> GetTokenBalances(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "tokens", tokenCode, "balance");
            return await builder.ExecuteGet<TokenBalancesResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<IEnumerable<TokenFeePayerResponse>> GetTokenFeePayerTotals()
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "feepayeraccounts", "totals");
            return await builder.ExecuteGet<List<TokenFeePayerResponse>>();
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
        /// Lists token operations based on the query parameters
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of token payments, fundings, payouts and clawbacks
        /// </returns>
        public async Task<PagedResponse<TokenOperationResponse>> GetTokenPayments(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "operations");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<TokenOperationResponse>>();
        }

        /// <inheritdoc/>
        public async Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests, bool awaitResult = true,
            CancellationToken cancellationToken = default)
        {
            foreach (var request in requests)
            {
                var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "envelope", "signature", "submit");
                await builder.ExecutePost(request, cancellationToken);
            }

            if (awaitResult && requests.Any(r => r.BackgroundSubmit))
            {
                await Task.WhenAll(requests.Select(async request =>
                {
                    await WaitForCompletionAsync(request.TransactionHash, cancellationToken);
                }));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SubmitOnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests)
        {
            foreach (var request in requests)
            {
                var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "envelope", "signature", "submit");
                await builder.ExecutePost(request);
            }
        }

        /// <summary>
        /// Wait for the completion of a token operation.
        /// It must be fully processed and either Completed, Failed or Cancelled before returning.
        /// </summary>
        /// <param name="code">Unique Nexus identifier of the token operation or transaction hash</param>
        public async Task<bool> WaitForCompletionAsync(string code, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                // get the status of the envelope
                var envelope = await GetEnvelope(code);

                // if the status is completed, break
                var env = envelope;

                if (env != null)
                {
                    _logger.LogDebug("Waiting for result of envelope {code} with current status: {status}", code, env.status);

                    if (env.status == "Completed")
                    {
                        return true;
                    }

                    if (env.status is "Failed" or "Cancelled")
                    {
                        return false;
                    }
                }

                // else delay
                await Task.Delay(3000, cancellationToken);
            }
        }

        public async Task<EnvelopeResponse> GetEnvelope(string code)
        {
            var builder = new RequestBuilder(_client, _handler, _logger)
                .SetSegments("token", "envelope", code);
            return await builder.ExecuteGet<EnvelopeResponse>();
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

        public async Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("labelpartner", "datatemplates");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<CustomDataResponse>>();
        }

        public async Task<PagedResponse<MailsResponse>> GetMails(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("mail");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<MailsResponse>>();
        }

        public async Task<MailsResponse> UpdateMailSent(string code)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("mail", code, "sent");

            return await builder.ExecutePut<MailsResponse>();
        }

        public async Task<MailsResponse> CreateMail(CreateMailRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("mail");

            return await builder.ExecutePost<CreateMailRequest, MailsResponse>(request);
        }

        public async Task<PaymentMethodsResponse> GetPaymentMethod(string paymentMethodCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("paymentmethod", paymentMethodCode);
            return await builder.ExecuteGet<PaymentMethodsResponse>();
        }

        public async Task<NexusResponse> DeleteAccount(string accountCode)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("accounts", accountCode);
            return await builder.ExecuteDelete<NexusResponse>();
        }

        public async Task<TokenOperationResponse> UpdateOperationStatusAsync(string operationCode, string status, string? comment = null, string? customerIPAddress = null, string? paymentReference = null)
        {
            var builder = new RequestBuilder(_client, _handler, _logger).SetSegments("token", "operations", operationCode);

            if (customerIPAddress != null)
            {
                builder.AddHeader("customer_ip_address", customerIPAddress);
            }

            var request = new UpdateOperationStatusRequest
            {
                Status = status,
                PaymentReference = paymentReference
            };

            if (comment != null)
            {
                request.Comment = comment;
            }

            return await builder.ExecutePut<UpdateOperationStatusRequest, TokenOperationResponse>(request);
        }
    }
}
