namespace Nexus.SDK.Shared.Authentication
{
    public class AuthProviderOptions
    {
        public AuthProviderOptions(string identityUrl, string clientId, string clientSecret)
        {
            IdentityUrl = identityUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public AuthProviderOptions() { }

        public string? IdentityUrl { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
