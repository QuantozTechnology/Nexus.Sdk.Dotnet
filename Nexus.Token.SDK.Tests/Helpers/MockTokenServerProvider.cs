using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Tests.Helpers
{
    public class MockTokenServerProvider : ITokenServerProvider
    {
        public Task<SignableResponse> CancelOrder(string orderCode)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, IEnumerable<string> tokenCodes)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            return request.BankAccounts != null && request.Data != null
                ? Task.FromResult(new CustomerResponse(request.CustomerCode, request.TrustLevel!, request.CurrencyCode!, request.Email, request.Status!, request.BankAccounts[0].BankAccountNumber!, request.IsBusiness!.Value, request.Data))
                : Task.FromResult(new CustomerResponse(request.CustomerCode, request.TrustLevel!, request.CurrencyCode!, request.Email, request.Status!, null!, request.IsBusiness!.Value, null!));
        }


        public Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePaymentsAsync(IEnumerable<PaymentDefinition> definitions, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<TaxonomySchemaResponse> CreateTaxonomySchema(string code, string schema, string? name, string? description)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokenOnAlgorand(AlgorandTokenDefinition definition, AlgorandTokenSettings? settings = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition definition, StellarTokenSettings? settings = null)
        {
            throw new NotImplementedException();
        }

        public Task<CreateTokenResponse> CreateTokensOnStellarAsync(IEnumerable<StellarTokenDefinition> definitions, StellarTokenSettings? settings = null)
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

        public Task<TokenResponse> GetToken(string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TokenResponse>> GetTokens(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task SubmitOnAlgorandAsync(IEnumerable<AlgorandSubmitRequest> requests)
        {
            return Task.CompletedTask;
        }

        public Task SubmitOnStellarAsync(StellarSubmitRequest request)
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

        public Task<TokenOperationResponse> GetTokenPayment(string tokenPaymentCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TokenOperationResponse>> GetTokenPayments(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<TrustLevelsResponse>> GetTrustLevels(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<AccountResponse>> GetAccounts(IDictionary<string, string>? query)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponse> UpdateCustomer(CustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }
    }
}
