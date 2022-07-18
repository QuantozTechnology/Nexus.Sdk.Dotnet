using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class OperationsFacade : TokenServerFacade
{
    public OperationsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null,
                                         string? memo = null)
    {
        await _provider.CreateFundingAsync(accountCode, tokenCode, amount, pm, memo);
    }

    public async Task CreateFundingAsync(string accountCode, FundingDefinition[] definitions, string? pm = null,
                                         string? memo = null)
    {
        await _provider.CreateFundingAsync(accountCode, definitions, pm, memo);
    }

    public async Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey,
                                                           string tokenCode, decimal amount, string? memo = null)
    {
        return await _provider.CreatePaymentAsync(senderPublicKey, receiverPublicKey, tokenCode, amount, memo);
    }

    public async Task<SignableResponse> CreatePaymentAsync(PaymentDefinition[] definitions, string? memo = null)
    {
        return await _provider.CreatePaymentAsync(definitions, memo);
    }

    public async Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount,
                                                    string? pm = null, string? memo = null)
    {
        return await _provider.CreatePayoutAsync(accountCode, tokenCode, amount, pm, memo);
    }

}
