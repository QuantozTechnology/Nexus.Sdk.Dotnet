using Nexus.SDK.Shared.Responses;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Responses;

namespace Nexus.Token.SDK.Facades
{
    public interface IOrdersFacade
    {
        public Task<PagedResponse<OrderResponse>> Get(IDictionary<string, string>? query);

        public Task<OrderResponse> Get(string orderCode);

        public Task<CreateOrderResponse> CreateOrder(OrderRequest orderRequest);

        public Task<SignableResponse> CancelOrder(string orderCode);
    }
}
