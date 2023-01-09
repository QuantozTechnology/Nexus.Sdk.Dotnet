using Nexus.SDK.Shared.Authentication;
using Nexus.SDK.Shared.ErrorHandling;

namespace Nexus.SDK.Shared.Tests.Helpers
{
    public class MockAuthProviderWithSuccess : IAuthProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            return Task.FromResult("access_token");
        }
    }

    public class MockAuthProviderWithException : IAuthProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            throw new AuthProviderException("No access token");
        }
    }
}
