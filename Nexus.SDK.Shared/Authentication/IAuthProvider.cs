using Microsoft.Extensions.Logging;

namespace Nexus.Sdk.Shared.Authentication;

public interface IAuthProvider
{
    Task<string> GetAccessTokenAsync();
}