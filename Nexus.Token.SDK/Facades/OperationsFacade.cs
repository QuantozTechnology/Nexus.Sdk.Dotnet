using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class OperationsFacade : TokenServerFacade
{
    public OperationsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// Get token payment details based on the code
    /// </summary>
    /// <param name="tokenPaymentCode">Unique identifier of the payment</param>
    /// <returns></returns>
    public async Task<TokenOperationResponse> Get(string tokenPaymentCode)
    {
        return await _provider.GetTokenPayment(tokenPaymentCode);
    }

    /// <summary>
    /// Get token payments based on query parameters
    /// </summary>
    /// Possible Payment Types:
    /// - `Funding`
    /// - `Payment`
    /// - `FundingPath`
    /// - `Payout`
    /// - `Clawback`
    /// <param name="query"></param>
    /// <returns></returns>
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

    public async Task<SignableResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey,
                                                           string tokenCode, decimal amount, string? memo = null)
    {
        return await _provider.CreatePaymentAsync(senderPublicKey, receiverPublicKey, tokenCode, amount, memo);
    }

    public async Task<SignableResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null)
    {
        return await _provider.CreatePaymentsAsync(definitions, memo);
    }

    public async Task<SignableResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount,
                                                    string? pm = null, string? memo = null)
    {
        return await _provider.CreatePayoutAsync(accountCode, tokenCode, amount, pm, memo);
    }

}
