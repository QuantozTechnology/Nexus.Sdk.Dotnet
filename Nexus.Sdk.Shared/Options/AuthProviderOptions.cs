using System.ComponentModel.DataAnnotations;

namespace Nexus.Sdk.Shared.Options
{
    public class AuthProviderOptions : IValidatableObject
    {
        internal const string MissingCredentialsMessage = "Either AccessToken or IdentityUrl, ClientId and ClientSecret must be provided.";

        public string IdentityUrl { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public string? Scopes { get; set; }

        /// <summary>
        /// Optional personal access token. When set, it is used instead of the client credentials flow.
        /// </summary>
        public string? AccessToken { get; set; }

        internal bool HasAccessToken => !string.IsNullOrWhiteSpace(AccessToken);

        internal bool HasClientCredentials =>
            !string.IsNullOrWhiteSpace(IdentityUrl)
            && !string.IsNullOrWhiteSpace(ClientId)
            && !string.IsNullOrWhiteSpace(ClientSecret);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!HasAccessToken && !HasClientCredentials)
            {
                yield return new ValidationResult(MissingCredentialsMessage, [nameof(AccessToken), nameof(IdentityUrl), nameof(ClientId), nameof(ClientSecret)]);
            }
        }
    }
}
