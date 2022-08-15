using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Requests;

namespace Nexus.SDK.Shared.Tests.Helpers
{
    public class MockRequestBuilder : RequestBuilder<MockRequestBuilder>
    {
        public MockRequestBuilder(IAuthProvider authProvider, HttpMessageHandler messageHandler)
            : base(new Uri("https://mockrequestbuilder.com"), new HttpClient(messageHandler), authProvider, null)
        {
        }

        public async Task<TResponse> Get<TResponse>(string[] segments, IDictionary<string, string>? queryParameters = null) where TResponse : class
        {
            SetSegments(segments);

            if (queryParameters != null)
            {
                SetQueryParameters(queryParameters);
            }

            return await ExecuteGet<TResponse>();
        }
    }
}
