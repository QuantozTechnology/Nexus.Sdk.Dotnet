using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Facades.Interfaces;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades;

public class BankAccountsFacade : TokenServerFacade, IBankAccountsFacade
{
    public BankAccountsFacade(ITokenServerProvider provider) : base(provider)
    {
    }

    public async Task<PagedResponse<BankAccountResponse>> Get(IDictionary<string, string> query)
    {
        return await _provider.GetBankAccounts(query);
    }

    public async Task<BankAccountResponse> Update(UpdateBankAccountRequest updateRequest, string? customerIPAddress = null)
    {
        return await _provider.UpdateBankAccount(updateRequest, customerIPAddress);
    }

    public async Task<BankAccountResponse> Create(CreateBankAccountRequest request, string? customerIPAddress = null)
    {
        return await _provider.CreateBankAccount(request, customerIPAddress);
    }

    public async Task Delete(DeleteBankAccountRequest request)
    {
        await _provider.DeleteBankAccount(request);
    }
}
