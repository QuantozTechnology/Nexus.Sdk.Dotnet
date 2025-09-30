using Nexus.Sdk.Shared;
using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token
{
    public interface ITokenServerProvider : IServerProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <returns></returns>
        Task<AccountResponse> GetAccount(string accountCode);

        Task<PagedResponse<AccountResponse>> GetAccounts(IDictionary<string, string>? query);

        /// <summary>
        /// Create a virtual account
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="address">Optionally supply a receive address to receive from blockchain accounts.</param>
        /// <param name="generateReceiveAddress">Generate a receive address for the virtual account to receive from blockchain accounts.</param>
        /// <param name="cryptoCode">Blockchain to connect this account to.</param>
        /// <param name="allowedTokens">A list of token codes the account will be connected to upon creation</param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <returns></returns>
        Task<AccountResponse> CreateVirtualAccount(string customerCode, string address, bool generateReceiveAddress, string cryptoCode, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
        /// <returns></returns>
        Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <param name="tokenCodes"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
        /// <returns></returns>
        Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
        /// <returns></returns>
        Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

        /// <summary>
        ///
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="publicKey"></param>
        /// <param name="tokenCodes"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="customName">Optional custom name for account</param>
        /// <param name="accountType">Optional type for account (Defaults to a managed account)</param>
        /// <returns></returns>
        Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED");

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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="tokenCodes"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, IEnumerable<string> tokenCodes, string? customerIPAddress = null);

        /// <summary>
        /// Updates account properties based on the code
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="customerCode"></param>
        /// <param name="updateRequest"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<SignableResponse> UpdateAccount(string customerCode, string accountCode, UpdateTokenAccountRequest updateRequest, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task<TokenDetailsResponse> GetToken(string tokenCode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task<TokenBalancesResponse> GetTokenBalances(string tokenCode);

        /// <summary>
        ///
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [Obsolete("Deprecated. Use GetTokenFeePayerDetails instead")]
        Task<IEnumerable<TokenFeePayerResponse>> GetTokenFeePayerTotals();

        Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? query);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);

        Task<CreateTokenResponse> CreateTokensOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definition"></param>
        /// <param name="settings"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="settings"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null, string? customerIPAddress = null);

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
        Task<FundingResponses> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? nonce = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountCode"></param>
        /// <param name="definitions"></param>
        /// <param name="pm"></param>
        /// <param name="memo"></param>
        /// <param name="message"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<FundingResponses> CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null, string? message = null, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="senderPublicKey"></param>
        /// <param name="receiverPublicKey"></param>
        /// <param name="tokenCode"></param>
        /// <param name="amount"></param>
        /// <param name="memo"></param>
        /// <param name="message"></param>
        /// <param name="cryptoCode"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <param name="blockchainTransactionId">Only provide the blockchain transaction ID if available and no onchain transaction should be created.</param>
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param> 
        /// <returns></returns>
        Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="definitions"></param>
        /// <param name="memo"></param>
        /// <param name="message"></param>
        /// <param name="cryptoCode"></param>
        /// <param name="callbackUrl"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<SignablePaymentResponse> CreatePaymentsAsync(IEnumerable<PaymentDefinition> definitions, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null);

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
        /// <param name="blockchainTransactionId">Only provide the blockchain transaction ID if available and no onchain transaction should be created.</param>
        /// <param name="nonce">Optional nonce value to prevent accidental duplicate transactions</param>
        /// <returns></returns>
        Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null);

        /// <summary>
        /// Simulate a payout operation without actually executing it
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
        Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null, string? blockchainTransactionId = null, string? nonce = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="transactionRequest"></param>
        /// <returns></returns>
        Task SubmitOnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests);

        /// <summary>
        /// Submit a signature for a token operation specific to Algorand
        /// </summary>
        /// <param name="requests">Collection of signatures to send</param>
        /// <param name="awaitResult">If true, the method will await for the submit to be fully processed</param>
        /// <returns></returns>
        Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests, bool awaitResult = true,
            CancellationToken cancellationToken = default);

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
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest, string? customerIPAddress = null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        Task<SignableResponse> CancelOrder(string orderCode);

        /// <summary>
        /// Lists token operations based on the query parameters
        /// </summary>
        /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of token payments, fundings, payouts and clawbacks
        /// </returns>
        Task<PagedResponse<TokenOperationResponse>> GetTokenPayments(IDictionary<string, string>? query);

        /// <summary>
        /// Get token funding limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <param name="blockchainCode">Blockchain code (ex. ETH instead of Ethereum) used to further identify the token.</param>
        /// <returns>
        /// The current spending limits expressed in token value.
        /// </returns>
        Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode, string? blockchainCode = null);

        /// <summary>
        /// Get token payout limits of customer
        /// </summary>
        /// <param name="customerCode">Unique Nexus identifier of the customer.</param>
        /// <param name="tokenCode">Unique Nexus identifier of the token.</param>
        /// <param name="blockchainCode">Blockchain code (ex. ETH instead of Ethereum) used to further identify the token.</param>
        /// <returns>
        /// The current fiat spending limits expressed in token value.
        /// </returns>
        Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode, string? blockchainCode = null);

        /// <summary>
        /// Get payment method data
        /// </summary>
        /// <param name="paymentMethodCode">Unique identifier of the payment method.</param>
        Task<PaymentMethodsResponse> GetPaymentMethod(string paymentMethodCode);

        /// <summary>
        /// Delete an account.
        /// Please note that all token balances needs to be 0 and all tokens disabled.
        /// </summary>
        /// <param name="accountCode">{crypto}-{publickey} combination of the account. E.g. XLM-GAW6GBLA5U4KCXV4E5SZTVERBF3AUASEPNTN4ZXSXLCROOTJ7KQQW4S7</param>
        Task<NexusResponse> DeleteAccount(string accountCode);

        /// <summary>
        /// Updates the status of a token operation.
        /// </summary>
        /// <param name="operationCode">Unique Nexus identifier of the operation.</param>
        /// <param name="status">New status of the operation.</param>
        /// <param name="comment">Optional comment explaining the reason for the update. Default comment: Operation updated.</param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions.</param>
        /// <param name="paymentReference">Optional reference to bank payment</param>
        /// <returns>The updated token operation response.</returns>
        Task<TokenOperationResponse> UpdateOperationStatusAsync(string operationCode, string status, string? comment = null, string? customerIPAddress = null, string? paymentReference = null);

        /// <summary>
        /// Get envelope
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<EnvelopeResponse> GetEnvelope(string code);

        /// <summary>
        /// Check for completion of an envelope by periodically polling the server
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> WaitForCompletionAsync(string code, CancellationToken cancellationToken = default);

        /// <summary>
        /// List bank accounts based on the query parameters
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
        /// <returns>
        /// Return a paged list of bank accounts
        /// </returns>
        Task<PagedResponse<BankAccountResponse>> GetBankAccounts(IDictionary<string, string>? query);

        /// <summary>
        /// Create bank account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<BankAccountResponse> CreateBankAccount(CreateBankAccountRequest request, string? customerIPAddress = null);

        /// <summary>
        /// Update bank account
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <param name="customerIPAddress">Optional IP address of the customer used for tracing their actions</param>
        /// <returns></returns>
        Task<BankAccountResponse> UpdateBankAccount(UpdateBankAccountRequest updateRequest, string? customerIPAddress = null);

        /// <summary>
        /// Delete bank account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task DeleteBankAccount(DeleteBankAccountRequest request);

        /// <summary>
        /// Retrieve the Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task<DocumentStoreSettingsResponse> GetDocumentStore(string customerIPAddress);

        /// <summary>
        /// Create a new Document Store with the provided settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task CreateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress);

        /// <summary>
        /// Update the existing Document Store settings
        /// </summary>
        /// <param name="documentStoreSettings"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task UpdateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress);

        /// <summary>
        /// Delete the existing Document Store settings
        /// </summary>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task DeleteDocumentStore(string customerIPAddress);

        /// <summary>
        /// Retrieve a list of files in the Document Store based on the provided query parameters.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>        
        Task<PagedResponse<DocumentStoreItemResponse>> GetDocumentStoreFileList(IDictionary<string, string>? queryParameters, string customerIPAddress);

        /// <summary>
        /// Upload a document to the Document Store.
        /// </summary>
        /// <param name="fileUploadRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task AddDocumentToStore(FileUploadRequest fileUploadRequest, string customerIPAddress);

        /// <summary>
        /// Retrieve a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>        
        Task<Stream> GetDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress);

        /// <summary>
        /// Delete a document from the Document Store based on the provided file path.
        /// </summary>
        /// <param name="documentRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task DeleteDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress);

        /// <summary>
        /// Update document metadata in the Document Store
        /// </summary>
        /// <param name="fileUpdateRequest"></param>
        /// <param name="customerIPAddress"></param>
        /// <returns></returns>
        Task UpdateDocumentInStore(FileUpdateRequest fileUpdateRequest, string customerIPAddress);


        /// <summary>
        /// List fee payers based on query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters</param>
        /// <returns>List of fee payers based on the query parameters.</returns>
        Task<PagedResponse<FeePayerDetailsResponse>> GetTokenFeePayerDetails(IDictionary<string, string> queryParameters);
    }
}
