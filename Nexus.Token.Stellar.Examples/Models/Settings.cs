using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Token.Algorand.Examples.Models
{
    public sealed class Settings
    {
        public string NexusApiUrl { get; set; }
        public string NexusIdentityUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string FundingPaymentMethod { get; set; }
        public string PayoutPaymentMethod { get; set; }
        public string NetworkPassphrase { get; set; }
    }
}
