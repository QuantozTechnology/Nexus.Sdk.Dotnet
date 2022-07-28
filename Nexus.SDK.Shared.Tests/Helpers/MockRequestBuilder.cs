using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Requests;

namespace Nexus.SDK.Shared.Tests.Helpers
{
    public class MockRequestBuilder : RequestBuilder<MockRequestBuilder>
    {
        public MockRequestBuilder(IAuthProvider authProvider,
            ILogger<RequestBuilder<MockRequestBuilder>>? logger = null)
            : base(new Uri("https://mockrequestbuilder.com"), new HttpClient(), authProvider, logger)
        {
        }

        public async Task<TResponse> Get<TResponse>(params string[] segments) where TResponse : class
        {
            SetSegments(segments);
            return await base.ExecuteGet<TResponse>();
        }
    }
}
