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

        public required FundingPaymentMethodOptions FundingPaymentMethodOptions { get; set; }

    }

    public class FundingPaymentMethodOptions
    {
        public required string Consumer { get; set; }
        public required string Business { get; set; }
    }
}
