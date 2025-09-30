﻿using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Tests.Helpers
{
    public class MockTokenServerProvider : ITokenServerProvider
    {
        public Task<SignableResponse> CancelOrder(string orderCode)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, IEnumerable<string> tokenCodes, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, string? customerIPAddress = null, string? customName = null, string? accountType = "MANAGED")
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, string? customerIPAddress = null)
        {
            string bankAccountNumber = "";
            if (request.BankAccounts != null && request.BankAccounts.Any())
            {
                bankAccountNumber = request.BankAccounts[0]?.BankAccountNumber ?? "";
            }

            return Task.FromResult(new CustomerResponse(
                request.CustomerCode!,
                request.Name!,
                request.FirstName!,
                request.LastName!,
                request.DateOfBirth!,
                request.Phone!,
                request.CompanyName!,
                request.TrustLevel!,
                request.CurrencyCode!,
                request.Address,
                request.City,
                request.ZipCode,
                request.State,
                request.CountryCode!,
                request.Email,
                request.Status!,
                bankAccountNumber,
                request.IsBusiness!,
                request.RiskQualification!,
                request.Created!,
                request.PortFolioCode!,
                request.ExternalCustomerCode!,
                request.IsReviewRecommended,
                request.IsPEP!,
                request.Data!
            ));
        }

        public Task<FundingResponses> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? nonce = null)
        {
            throw new NotImplementedException();
        }

        public Task<FundingResponses> CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null, string? message = null, string? customerIPAddress = null)
        {
            var fundingResponses = new FundingResponses
            {
                PaymentMethod = new PaymentMethodInfo
                {
                    PaymentMethodName = "MockPaymentMethod"
                },
                Funding = new List<FundingResponse>
                {
                    new() {
                        TokenCode = "MockTokenCode",
                        FundingPaymentCode = "MockFundingPaymentCode",
                        RequestedAmount = 100,
                        ExecutedAmount = 100,
                        ServiceFee = 5,
                        BankFee = 2,
                        PaymentReference = "MockPaymentReference"
                    }
                },
                TransactionEnvelope = new TxEnvelopeResponse
                {
                    Code = "MockEnvelopeCode",
                    Hash = "MockEnvelopeHash",
                    SignedTransactionEnvelope = "MockSignedTransactionEnvelope",
                    SigningNeeded = false,
                    Status = "Completed"
                }
            };

            return Task.FromResult(fundingResponses);
        }

        public Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignablePaymentResponse> CreatePaymentsAsync(IEnumerable<PaymentDefinition> definitions, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            throw new NotImplementedException();
        }

        public Task<TaxonomySchemaResponse> CreateTaxonomySchema(string code, string schema, string? name, string? description)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokensOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(string customerCode)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetAccount(string accountCode)
        {
            throw new NotImplementedException();
        }

        public Task<AccountBalancesResponse> GetAccountBalanceAsync(string accountCode)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponse> GetCustomer(string customerCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<CustomerResponse>> GetCustomers(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponse> GetOrder(string orderCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<OrderResponse>> GetOrders(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<TaxonomyResponse> GetTaxonomy(string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<TaxonomySchemaResponse> GetTaxonomySchema(string code)
        {
            throw new NotImplementedException();
        }

        public Task<TokenDetailsResponse> GetToken(string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<TokenBalancesResponse> GetTokenBalances(string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TokenFeePayerResponse>> GetTokenFeePayerTotals()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitSignatureRequest> requests, bool waitForCompletion = true, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SubmitOnStellarAsync(IEnumerable<StellarSubmitSignatureRequest> requests)
        {
            throw new NotImplementedException();
        }

        public Task<TaxonomySchemaResponse> UpdateTaxonomySchema(string code, string? name = null, string? description = null, string? schema = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey, IEnumerable<string> tokenCodes)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokensOnAlgorand(IEnumerable<AlgorandTokenDefinition> definitions, AlgorandTokenSettings? settings = null)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDataResponse> GetCustomerData(string customerCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TokenOperationResponse>> GetTokenPayment(string tokenPaymentCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TokenOperationResponse>> GetTokenPayments(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            throw new NotImplementedException();
        }

        public Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode, string? blockchainCode = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<CustomDataResponse>> GetCustomDataTemplates(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<MailsResponse>> GetMails(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<AccountResponse>> GetAccounts(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest request, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteCustomerResponse> DeleteCustomer(DeleteCustomerRequest request, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null, string? blockchainTransactionId = null, string? nonce = null)
        {
            throw new NotImplementedException();
        }

        public Task<MailsResponse> UpdateMailSent(string code)
        {
            throw new NotImplementedException();
        }

        public Task<MailsResponse> CreateMail(CreateMailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> UpdateAccount(string customerCode, string accountCode, UpdateTokenAccountRequest updateRequest, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentMethodsResponse> GetPaymentMethod(string paymentMethodCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<CustomerTraceResponse>> GetCustomerTrace(string customerCode, IDictionary<string, string>? queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<NexusResponse> DeleteAccount(string accountCode)
        {
            throw new NotImplementedException();
        }

        public Task<TokenOperationResponse> UpdateOperationStatusAsync(string operationCode, string status, string? comment = null, string? customerIPAddress = null, string? paymentReference = null)
        {
            var tokenOperationResponse = new TokenOperationResponse
            (
                code: "MockCode",
                hash: "MockHash",
                senderAccount: new OperationAccountResponses(
                    "MockSenderCustomerCode",
                    "MockSenderAccountCode",
                    "MockSenderPublicKey"
                ),
                receiverAccount: new OperationAccountResponses(
                    "MockSenderCustomerCode",
                    "MockSenderAccountCode",
                    "MockSenderPublicKey"
                ),
                amount: 100,
                created: "MockCreated",
                finished: "MockFinished",
                status: status,
                type: "MockType",
                memo: "MockMemo",
                message: "MockMessage",
                cryptoCode: "MockCryptoCode",
                tokenCode: "MockTokenCode",
                paymentReference: paymentReference ?? "MockPaymentReference",
                fiatAmount: 100,
                netFiatAmount: 100,
                blockchainTransactionId: "MockBlockchainTransactionId",
                fees: new OperationFees
                {
                    BankFees = new OperationBankFees
                    {
                        TotalFiat = 100
                    },
                    PartnerFees = new OperationPartnerFees
                    {
                        TotalFiat = 100
                    },
                    NetworkFees = new OperationNetworkFees
                    {
                        EstimatedFiat = 100,
                        EstimatedCrypto = 100
                    }
                },
                bankAccountNumber: "MockBankAccountNumber",
                nonce: "MockNonce"
            );

            return Task.FromResult(tokenOperationResponse);
        }

        public Task<EnvelopeResponse> GetEnvelope(string code)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WaitForCompletionAsync(string hash, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<BankAccountResponse>> GetBankAccounts(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccountResponse> CreateBankAccount(CreateBankAccountRequest request, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccountResponse> UpdateBankAccount(UpdateBankAccountRequest updateRequest, string? customerIPAddress = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBankAccount(DeleteBankAccountRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> CreateVirtualAccount(string customerCode, string address, bool generateReceiveAddress, string cryptoCode, IEnumerable<string> allowedTokens, string? customerIPAddress = null, string? customName = null)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentStoreSettingsResponse> GetDocumentStore(string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task CreateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDocumentStore(DocumentStoreSettingsRequest documentStoreSettings, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDocumentStore(string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<DocumentStoreItemResponse>> GetDocumentStoreFileList(IDictionary<string, string>? queryParameters, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task AddDocumentToStore(FileUploadRequest fileUploadRequest, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            throw new NotImplementedException();
        }
        public Task DeleteDocumentFromStore(DocumentRequest documentRequest, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDocumentInStore(FileUpdateRequest fileUpdateRequest, string customerIPAddress)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<FeePayerDetailsResponse>> GetTokenFeePayerDetails(IDictionary<string, string> queryParameters)
        {
            throw new NotImplementedException();
        }
    }
}
