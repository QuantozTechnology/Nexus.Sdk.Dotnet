using Nexus.SDK.Shared.Authentication;

namespace Nexus.SDK.Shared.Tests.Helpers
{
    public class MockAuthProviderWithSuccess : IAuthProvider
    {
        public Task AuthenticateAsync(HttpClient client)
        {
            return Task.CompletedTask;
        }
    }

    public class MockAuthProviderWithException : IAuthProvider
    {
        public Task AuthenticateAsync(HttpClient client)
        {
            throw new AuthProviderException("");
        }
    }
}
