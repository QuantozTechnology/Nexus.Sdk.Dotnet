using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public class OperationsFacade : TokenServerFacade, IOperationsFacade
{
    public OperationsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<PagedResponse<TokenOperationResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetTokenPayments(query);
    }

    public async Task<FundingResponses> CreateFundingAsync(string accountCode, string tokenCode, decimal amount, string? pm = null,
                                         string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null)
    {
        return await _provider.CreateFundingAsync(accountCode, tokenCode, amount, pm, memo, message, paymentReference, customerIPAddress); ;
    }

    public async Task<FundingResponses> CreateFundingAsync(string accountCode, IEnumerable<FundingDefinition> definitions, string? pm = null,
                                         string? memo = null, string? message = null, string? customerIPAddress = null)
    {
        return await _provider.CreateFundingAsync(accountCode, definitions, pm, memo, message, customerIPAddress);
    }

    public async Task<SignablePaymentResponse> CreatePaymentAsync(string senderPublicKey, string receiverPublicKey,
                                                           string tokenCode, decimal amount, string? memo = null,
                                                           string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null)
    {
        return await _provider.CreatePaymentAsync(senderPublicKey, receiverPublicKey, tokenCode, amount, memo, message, cryptoCode, callbackUrl, customerIPAddress);
    }

    public async Task<SignablePaymentResponse> CreatePaymentsAsync(PaymentDefinition[] definitions, string? memo = null, string? message = null, string? cryptoCode = null, string? callbackUrl = null, string? customerIPAddress = null)
    {
        return await _provider.CreatePaymentsAsync(definitions, memo, message, cryptoCode, callbackUrl, customerIPAddress);
    }

    public async Task<SignablePayoutResponse> CreatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? message = null, string? paymentReference = null, string? customerIPAddress = null, string? blockchainTransactionId = null)
    {
        return await _provider.CreatePayoutAsync(accountCode, tokenCode, amount, pm, memo, message, paymentReference, customerIPAddress, blockchainTransactionId);
    }

    public async Task<PayoutOperationResponse> SimulatePayoutAsync(string accountCode, string tokenCode, decimal amount, string? pm = null, string? memo = null, string? paymentReference = null, string? blockchainTransactionId = null)
    {
        return await _provider.SimulatePayoutAsync(accountCode, tokenCode, amount, pm, memo, paymentReference, blockchainTransactionId);
    }
}
