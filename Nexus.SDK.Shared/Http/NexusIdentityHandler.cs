using System.Net.Http.Headers;
using Nexus.Sdk.Shared.Authentication;

namespace Nexus.Sdk.Shared.Http
{
    public class NexusIdentityHandler : DelegatingHandler
    {
        private readonly IAuthProvider _provider;

        public NexusIdentityHandler(IAuthProvider provider)
        {
            _provider = provider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _provider.GetAccessTokenAsync();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
