using Nexus.Sdk.Shared.Responses;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;

namespace Nexus.Sdk.Token.Facades
{
    public interface IOrdersFacade
    {
        public Task<PagedResponse<OrderResponse>> Get(IDictionary<string, string>? query);

        public Task<OrderResponse> Get(string orderCode);

        public Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest, string? customerIPAddress = null);

        public Task<SignableResponse> CancelOrder(string orderCode);
    }
}
