namespace Nexus.SDK.Shared.Authentication;

public class AuthProviderException : Exception
{
    public AuthProviderException(string error)
        : base(string.Format("An unexpected error occured during authorization: {0}", error))
    {
    }
}
