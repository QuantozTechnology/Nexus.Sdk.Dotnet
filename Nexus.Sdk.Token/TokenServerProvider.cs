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
    public class TokenServerProvider(IHttpClientFactory factory, NexusOptions options, ILogger<TokenServerProvider> logger) : ITokenServerProvider
    {
        private readonly HttpClient _client = factory.CreateClient("NexusApi");
        private readonly NexusResponseHandler _handler = new(logger);
        private readonly Dictionary<string, string> _headers = [];

        /// <summary>
        /// Add a header to the request
        /// </summary>
        public void AddHeader(string key, string value)
        {
            _headers.Add(key, value);
        }

        public async Task<SignableResponse> CancelOrder(string orderCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "orders", "cancel");
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("accounts", accountCode)
                .AddHeader("customer_ip_address", customerIPAddress);

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
        public async Task<AccountResponse> CreateVirtualAccount(string customerCode, string address, bool generateReceiveAddress, string cryptoCode, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null)
        {
            if (!string.IsNullOrWhiteSpace(address) && generateReceiveAddress)
            {
                throw new InvalidOperationException("Supplying an address and requesting one to be generated is unsupported.");
            }

            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts")
                .AddHeader("customer_ip_address", customerIPAddress);

            var request = new CreateVirtualAccountRequest
            {
                GenerateReceiveAddress = generateReceiveAddress,
                Address = address,
                AccountType = "VIRTUAL",
                CryptoCode = cryptoCode,
                TokenSettings = new CreateTokenAccountSettings
                {
                    AllowedTokens = allowedTokens
                }
            };

            if (!string.IsNullOrWhiteSpace(customName))
            {
                request.CustomName = customName;
            }

            return await builder.ExecutePost<CreateVirtualAccountRequest, AccountResponse>(request);
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts")
                .AddHeader("customer_ip_address", customerIPAddress);

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

        public async Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "accounts", accountCode)
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", request.CustomerCode!)
                .AddHeader("customer_ip_address", customerIPAddress);

            return await builder.ExecutePut<UpdateCustomerRequest, CustomerResponse>(request);
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        public async Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", request.CustomerCode!)
                .AddHeader("customer_ip_address", customerIPAddress);

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
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param>
        /// <param name="bankAccountNumber">Bank account number of customer to be linked to this funding.</param>
        /// <returns></returns>
        public async Task<FundingResponses> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? nonce = null, string? bankAccountNumber = null)
        {
            var definition = new FundingDefinition(tokenCode, amount, paymentReference, nonce, bankAccountNumber);
            return await CreateFundingAsync(accountCode, [definition], pm, memo, message, customerIPAddress);
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
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(options.PaymentMethodOptions.Funding))
            {
                throw new InvalidOperationException("Funding payment method is required to fund an account with tokens");
            }

            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "fund")
                .AddHeader("customer_ip_address", customerIPAddress);

            var account = await GetAccount(accountCode);

            var request = new FundingOperationRequest
            {
                CustomerCode = account.CustomerCode,
                AccountCode = accountCode,
                Definitions = definitions,
                Memo = memo,
                Message = message,
                PaymentMethodCode = (pm ?? options.PaymentMethodOptions.Funding)!
            };

            return await builder.ExecutePost<FundingOperationRequest, FundingResponses>(request);
        }

        public async Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "orders")
                .AddHeader("customer_ip_address", customerIPAddress);

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
        /// <param name="blockchainTransactionId">Only provide the blockchain transaction ID if available and no onchain transaction should be created.</param>
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param>
        /// <returns></returns>
        public async Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            var definition = new PaymentDefinition(senderPublicKey, receiverPublicKey, tokenCode, amount, blockchainTransactionId, nonce);
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "payments")
                .AddHeader("customer_ip_address", customerIPAddress);

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
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(options.PaymentMethodOptions.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "payouts")
                .AddHeader("customer_ip_address", customerIPAddress);

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? options.PaymentMethodOptions.Payout,
                Amount = amount,
                TokenCode = tokenCode,
                PaymentReference = paymentReference,
                Memo = memo,
                Message = message,
                BlockchainTransactionId = blockchainTransactionId,
                Nonce = nonce
            };

            return await builder.ExecutePost<PayoutOperationRequest, SignablePayoutResponse>(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <param name="paymentReference"></param>
        /// <param name="blockchainTransactionId"></param>
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            if (string.IsNullOrWhiteSpace(pm) && string.IsNullOrWhiteSpace(options.PaymentMethodOptions.Payout))
            {
                throw new InvalidOperationException("Payout payment method is required for an account to payout a token");
            }

            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "payouts", "simulate");

            var request = new PayoutOperationRequest
            {
                AccountCode = accountCode,
                PaymentMethodCode = pm ?? options.PaymentMethodOptions.Payout,
                Amount = amount,
                TokenCode = tokenCode,
                PaymentReference = paymentReference,
                Memo = memo,
                BlockchainTransactionId = blockchainTransactionId,
                Nonce = nonce
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("taxonomy", "schema");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "tokens")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "tokens")
                .AddHeader("customer_ip_address", customerIPAddress);

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("accounts", accountCode);
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("accounts");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("accounts", accountCode, "tokenBalance");
            return await builder.ExecuteGet<AccountBalancesResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> GetCustomer(string customerCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode);
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "personalData");
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", customerCode, "trace");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "orders", orderCode);
            return await builder.ExecuteGet<OrderResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<OrderResponse>> GetOrders(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "orders");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("taxonomy", "token", tokenCode);
            return await builder.ExecuteGet<TaxonomyResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxonomySchemaCode"></param>
        /// <returns></returns>
        public async Task<TaxonomySchemaResponse> GetTaxonomySchema(string taxonomySchemaCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("taxonomy", "schema", taxonomySchemaCode);
            return await builder.ExecuteGet<TaxonomySchemaResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<TokenDetailsResponse> GetToken(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "tokens", tokenCode);
            return await builder.ExecuteGet<TokenDetailsResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task<TokenBalancesResponse> GetTokenBalances(string tokenCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "tokens", tokenCode, "balance");
            return await builder.ExecuteGet<TokenBalancesResponse>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<IEnumerable<TokenFeePayerResponse>> GetTokenFeePayerTotals()
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "feepayeraccounts", "totals");
            return await builder.ExecuteGet<List<TokenFeePayerResponse>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        public async Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "tokens");

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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "operations");

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
                var builder = new RequestBuilder(_client, _handler, logger, _headers)
                    .SetSegments("token", "envelope", "signature", "submit");
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
                var builder = new RequestBuilder(_client, _handler, logger, _headers)
                    .SetSegments("token", "envelope", "signature", "submit");
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
                    logger.LogDebug("Waiting for result of envelope {code} with current status: {status}", code, env.Status);

                    if (env.Status == "Completed")
                    {
                        return true;
                    }

                    if (env.Status is "Failed" or "Cancelled")
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
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
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("taxonomy", "schema", taxonomySchemaCode);

            var request = new UpdateTaxonomySchemaRequest
            {
                Name = name,
                Description = description,
                Schema = schema
            };

            return await builder.ExecutePut<UpdateTaxonomySchemaRequest, TaxonomySchemaResponse>(request);
        }

        public async Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers);
            if (!string.IsNullOrWhiteSpace(blockchainCode))
            {
                builder.SetSegments("customer", customerCode, "limits", "tokenfunding", "token", tokenCode, "blockchain", blockchainCode);
            }
            else
            {
                builder.SetSegments("customer", customerCode, "limits", "tokenfunding", "token", tokenCode);
            }
            return await builder.ExecuteGet<TokenLimitsResponse>();
        }

        public async Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers);
            if (!string.IsNullOrWhiteSpace(blockchainCode))
            {
                builder.SetSegments("customer", customerCode, "limits", "tokenpayout", "token", tokenCode, "blockchain", blockchainCode);
            }
            else
            {
                builder.SetSegments("customer", customerCode, "limits", "tokenpayout", "token", tokenCode);
            }
            return await builder.ExecuteGet<TokenLimitsResponse>();
        }

        public async Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("labelpartner", "trustlevels");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<TrustLevelsResponse>>();
        }

        public async Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("labelpartner", "datatemplates");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<CustomDataResponse>>();
        }

        public async Task<PagedResponse<MailsResponse>> GetMails(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("mail");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<MailsResponse>>();
        }

        public async Task<MailsResponse> UpdateMailSent(string code)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("mail", code, "sent");

            return await builder.ExecutePut<MailsResponse>();
        }

        public async Task<MailsResponse> CreateMail(CreateMailRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("mail");

            return await builder.ExecutePost<CreateMailRequest, MailsResponse>(request);
        }

        public async Task<PaymentMethodsResponse> GetPaymentMethod(string paymentMethodCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("paymentmethod", paymentMethodCode);
            return await builder.ExecuteGet<PaymentMethodsResponse>();
        }

        public async Task<NexusResponse> DeleteAccount(string accountCode)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("accounts", accountCode);
            return await builder.ExecuteDelete<NexusResponse>();
        }

        public async Task<TokenOperationResponse> UpdateOperationStatusAsync(string operationCode, string status, string? comment = null, string? customerIPAddress = null, string? paymentReference = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "operations", operationCode)
                .AddHeader("customer_ip_address", customerIPAddress);

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

        /// <summary>
        /// List bank accounts based on the query parameters
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of bank accounts
        /// </returns>
        public async Task<PagedResponse<BankAccountResponse>> GetBankAccounts(IDictionary<string, string>? queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", "bankaccounts");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<BankAccountResponse>>();
        }

        /// <summary>
        /// Create bank account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<BankAccountResponse> CreateBankAccount(CreateBankAccountRequest request, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", "bankAccounts")
                .AddHeader("customer_ip_address", customerIPAddress);

            return await builder.ExecutePost<CreateBankAccountRequest, BankAccountResponse>(request);
        }

        /// <summary>
        /// Update bank account
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        public async Task<BankAccountResponse> UpdateBankAccount(UpdateBankAccountRequest updateRequest, string? customerIPAddress = null)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", "bankAccounts")
                .AddHeader("customer_ip_address", customerIPAddress);

            return await builder.ExecutePut<UpdateBankAccountRequest, BankAccountResponse>(updateRequest);
        }

        /// <summary>
        /// Delete bank account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task DeleteBankAccount(DeleteBankAccountRequest request)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("customer", "bankAccounts");

            await builder.ExecuteDelete(request);
        }

        /// <summary>
        /// Retrieve the Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task<DocumentStoreSettingsResponse> GetDocumentStore(string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore")
                .AddHeader("customer_ip_address", customerIPAddress);

            return await builder.ExecuteGet<DocumentStoreSettingsResponse>();
        }

        /// <summary>
        /// Create a new Document Store with the provided settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task CreateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore")
                .AddHeader("customer_ip_address", customerIPAddress);

            await builder.ExecutePost<DocumentStoreSettingsRequest>(documentStoreSettings);
        }

        /// <summary>
        /// Update the existing Document Store settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task UpdateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore")
                .AddHeader("customer_ip_address", customerIPAddress);

            await builder.ExecutePut<DocumentStoreSettingsRequest, NexusResponse>(documentStoreSettings);
        }

        /// <summary>
        /// Delete the existing Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task DeleteDocumentStore(string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore")
                .AddHeader("customer_ip_address", customerIPAddress);

            await builder.ExecuteDelete<NexusResponse>();
        }

        /// <summary>
        /// Retrieve a list of files in the Document Store based on the provided query parameters.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task<PagedResponse<DocumentStoreItemResponse>> GetDocumentStoreFileList(IDictionary<string, string>? queryParameters, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore", "list")
                .AddHeader("customer_ip_address", customerIPAddress);

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<DocumentStoreItemResponse>>();
        }

        /// <summary>
        /// Upload a document to the Document Store.
        /// </summary>
        /// <param name="fileUploadRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task AddDocumentToStore(FileUploadRequest fileUploadRequest, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore", "file")
                .AddHeader("customer_ip_address", customerIPAddress);

            using var formContent = new MultipartFormDataContent();

            // Add file content
            using var fileStream = fileUploadRequest.File.OpenReadStream();
            using var fileContent = new StreamContent(fileStream);
            formContent.Add(fileContent, "file", fileUploadRequest.File.FileName);

            formContent.Add(new StringContent(fileUploadRequest.FilePath), "filePath");
            formContent.Add(new StringContent(fileUploadRequest.CustomerCode), "customerCode");
            formContent.Add(new StringContent(fileUploadRequest.DocumentTypeCode), "documentTypeCode");

            if (!string.IsNullOrEmpty(fileUploadRequest.Alias))
                formContent.Add(new StringContent(fileUploadRequest.Alias), "alias");

            if (!string.IsNullOrEmpty(fileUploadRequest.Description))
                formContent.Add(new StringContent(fileUploadRequest.Description), "description");

            if (!string.IsNullOrEmpty(fileUploadRequest.ItemReference))
                formContent.Add(new StringContent(fileUploadRequest.ItemReference), "itemReference");

            await builder.ExecutePost<NexusResponse>(formContent);
        }

        /// <summary>
        /// Retrieve a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task<Stream> GetDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore", "file")
                .AddHeader("customer_ip_address", customerIPAddress);

            builder.SetQueryParameters(new Dictionary<string, string>
            {
                { "filePath", documentRequest.FilePath }
            });

            return await builder.ExecuteGetStream();
        }

        /// <summary>
        /// Delete a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task DeleteDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore", "file")
                .AddHeader("customer_ip_address", customerIPAddress);

            builder.SetQueryParameters(new Dictionary<string, string>
            {
                { "filePath", documentRequest.FilePath }
            });

            await builder.ExecuteDelete<NexusResponse>();
        }

        /// <summary>
        /// Update document metadata in the Document Store.
        /// </summary>
        /// <param name="fileUpdateRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        public async Task UpdateDocumentInStore(FileUpdateRequest fileUpdateRequest, string customerIPAddress)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("integrations", "documentstore", "file")
                .AddHeader("customer_ip_address", customerIPAddress);

            await builder.ExecutePut<FileUpdateRequest, NexusResponse>(fileUpdateRequest);
        }

        /// <summary>
        /// List fee payers based on query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters</param>
        /// <returns>List of fee payers based on the query parameters.</returns>
        public async Task<PagedResponse<FeePayerDetailsResponse>> GetTokenFeePayerDetails(IDictionary<string, string> queryParameters)
        {
            var builder = new RequestBuilder(_client, _handler, logger, _headers)
                .SetSegments("token", "feePayers");

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<PagedResponse<FeePayerDetailsResponse>>();
        }
    }
}