﻿using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Facades;

public class CustomersFacade : ServerFacade
{
    public CustomersFacade(IServerProvider provider) : base(provider)
    {
    }

    public async Task<CustomerResponse> Get(string customerCode)
    {
        return await _provider.GetCustomer(customerCode);
    }

    public async Task<CreateCustomerResponse> Create(string code, string trustLevel, string currency)
    {
        return await _provider.CreateCustomer(code, trustLevel, currency);
    }
}