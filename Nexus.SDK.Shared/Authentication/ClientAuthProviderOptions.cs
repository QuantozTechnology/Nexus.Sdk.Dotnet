namespace Nexus.SDK.Shared.Authentication
{
    public class ClientAuthProviderOptions
    {
        public ClientAuthProviderOptions(string identityUrl, string clientId, string clientSecret)
        {
            IdentityUrl = identityUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string IdentityUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
