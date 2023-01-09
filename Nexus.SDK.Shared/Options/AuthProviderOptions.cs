using System.ComponentModel.DataAnnotations;

namespace Nexus.SDK.Shared.Options
{
    public class AuthProviderOptions
    {
        [Required]
        public required string IdentityUrl { get; set; }

        [Required]
        public required string ClientId { get; set; }

        [Required]
        public required string ClientSecret { get; set; }
    }
}
