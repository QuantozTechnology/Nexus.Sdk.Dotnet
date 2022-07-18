using Nexus.SDK.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.SDK.Shared
{
    public interface IServerProvider
    {
        public Task<CustomerResponse> GetCustomer(string customerCode);
        public Task<CreateCustomerResponse> CreateCustomer(string code, string trustLevel, string currency);
    }
}
