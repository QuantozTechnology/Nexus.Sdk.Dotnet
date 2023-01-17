using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades;

public class OperationsFacade : TokenServerFacade, IOperationsFacade
{
    public OperationsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    /// <summary>
    /// Get token operation details based on the code
    /// </summary>
    /// <param name="code">Unique Nexus identifier of the operation.</param>
    /// <returns>
    /// Return token operation details
    /// </returns>
    public async Task<TokenOperationResponse> Get(string code)
    {
        return await _provider.GetTokenPayment(code);
    }

    /// <summary>
    /// Lists token operations based on the query parameters
    /// </summary>
    /// <param name="query">Query parameters to filter on. Check the Nexus API documentation for possible filtering parameters.</param>
    /// <returns>
    /// Return a paged list of token payments, fundings, payouts and clawbacks
    /// </returns>
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
