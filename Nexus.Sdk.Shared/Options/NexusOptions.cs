using System.ComponentModel.DataAnnotations;

namespace Nexus.Sdk.Shared.Options
{
    public class NexusOptions
    {
        [Required]
        public required string ApiUrl { get; set; }

        [Required]
        public required AuthProviderOptions AuthProviderOptions { get; set; }

        public required PaymentMethodOptions PaymentMethodOptions { get; init; }
    }
}
