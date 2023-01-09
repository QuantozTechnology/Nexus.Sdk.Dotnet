using Microsoft.Extensions.Logging;

namespace Nexus.SDK.Shared.Authentication;

public interface IAuthProvider
{
    Task<string> GetAccessTokenAsync();
}