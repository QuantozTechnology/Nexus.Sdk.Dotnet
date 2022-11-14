using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades
{
    public class TransactionsFacade : TokenServerFacade
    {
        public TransactionsFacade(ITokenServerProvider provider) : base(provider)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public async Task<TransactionResponse> Get(string transactionCode)
        {
            return await _provider.GetTokenPayment(transactionCode);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResponse<TransactionResponse>> Get(IDictionary<string, string> query)
        {
            return await _provider.GetTokenPayments(query);
        }
    }
}
