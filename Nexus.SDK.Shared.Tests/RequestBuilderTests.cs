
using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.Tests.Helpers;

namespace Nexus.SDK.Shared.Tests
{
    public class RequestBuilderTests
    {
        [Test]
        public void RequestBuilderTests_Segments_Reset_After_AuthException_Is_Thrown()
        {
            var authProvider = new MockAuthProviderWithException();
            var requestBuilder = new MockRequestBuilder(authProvider);

            // Verify that the first GET throws an authentication exceptions
            Assert.ThrowsAsync<AuthProviderException>(() => requestBuilder.Get<string>("birds"));

            // Verify that the second GET also throws an authentication exception and not new Exception("URL segments have already been added.");
            Assert.ThrowsAsync<AuthProviderException>(() => requestBuilder.Get<string>("birds"));
        }
    }
}