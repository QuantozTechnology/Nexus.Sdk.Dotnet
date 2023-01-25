using Nexus.SDK.Shared.Http;

namespace Nexus.SDK.Shared.Tests.Helpers
{
    public class MockRequestBuilder
    {
        private HttpClient _client;
        private NexusResponseHandler _handler;

        public MockRequestBuilder(HttpMessageHandler messageHandler)
        {
            _client = new HttpClient(messageHandler);
            _handler = new NexusResponseHandler();
            _client.BaseAddress = new Uri("https://mockrequestbuilder.com");
        }

        public async Task<TResponse> Get<TResponse>(string[] segments, IDictionary<string, string>? queryParameters = null) where TResponse : class
        {
            var builder = new RequestBuilder(_client,_handler, null).SetSegments(segments);

            if (queryParameters != null)
            {
                builder.SetQueryParameters(queryParameters);
            }

            return await builder.ExecuteGet<TResponse>();
        }
    }
}
