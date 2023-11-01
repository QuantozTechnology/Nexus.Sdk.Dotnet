using Nexus.Sdk.Shared.Authentication;
using Nexus.Sdk.Shared.ErrorHandling;

namespace Nexus.Sdk.Shared.Tests.Helpers
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
