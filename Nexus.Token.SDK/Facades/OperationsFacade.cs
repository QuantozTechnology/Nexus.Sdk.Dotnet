using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class OperationsFacade : TokenServerFacade, IOperationsFacade
{
    public OperationsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<TokenOperationResponse> Get(string code)
    {
        return await _provider.GetTokenPayment(code);
    }

    public async Task<PagedResponse<TokenOperationResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetTokenPayments(query);
    }

    public async Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null,
                                         string? memo = null)
    {
        await _provider.CreateFundingAsync(accountCode, tokenCode, amount, pm, memo);
    }

    public async Task CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null,
                                         string? memo = null)
    {
        await _provider.CreateFundingAsync(accountCode, definitions, pm, memo);
    }

    public async Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey,
                                                           string tokenCode, decimal amount, string? memo = null)
    {
        return await _provider.CreatePaymentAsync(senderPublicKey, receiverPublicKey, tokenCode, amount, memo);
    }

    public async Task<SignablePaymentResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null)
    {
        return await _provider.CreatePaymentsAsync(definitions, memo);
    }

    public async Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount,
                                                    string? pm = null, string? memo = null)
    {
        return await _provider.CreatePayoutAsync(accountCode, tokenCode, amount, pm, memo);
    }

    public async Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null)
    {
        return await _provider.SimulatePayoutAsync(accountCode, tokenCode, amount, pm, memo);
    }
}
