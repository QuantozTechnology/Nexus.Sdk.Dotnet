using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Tests.Helpers
{
    public class MockTokenServerProvider : ITokenServerProvider
    {
        public Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, string[] tokenCodes)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAccountResponse> CreateAccountOnAlgorandAsync(string customerCode, string publicKey)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAccountResponse> CreateAccountOnStellarAsync(string customerCode, string publicKey)
        {
            throw new NotImplementedException();
        }

        public Task<CreateCustomerResponse> CreateCustomer(string code, string trustLevel, string currency)
        {
            return Task.FromResult(new CreateCustomerResponse(code,  trustLevel, currency));
        }

        public Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task CreateFundingAsync(string accountCode, FundingDefinition[] definitions, string? pm = null, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey, string tokenCode, decimal amount, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePaymentAsync(PaymentDefinition[] definitions, string? memo = null)
        {
            throw new NotImplementedException();
        }

        public Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
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

        public Task<CreateTokenResponse> CreateTokenOnStellarAsync(StellarTokenDefinition[] definitions, StellarTokenSettings? settings = null)
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

        public Task<TokenResponse> GetToken(string tokenCode)
        {
            throw new NotImplementedException();
        }

        public Task SubmitOnAlgorandAsync(AlgorandSubmitRequest request)
        {
            throw new NotImplementedException();
        }

        public Task SubmitOnStellarAsync(StellarSubmitRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
