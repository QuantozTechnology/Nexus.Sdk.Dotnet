using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Token.Stellar.Examples.Models
{
    public class ExamplePayment
    {
        public ExamplePayment(string senderPrivateKey, string receiverPrivateKey, string tokenCode, decimal amount)
        {
            SenderPrivateKey = senderPrivateKey;
            ReceiverPrivateKey = receiverPrivateKey;
            TokenCode = tokenCode;
            Amount = amount;
        }

        public string SenderPrivateKey { get; set; }

        public string ReceiverPrivateKey { get; set; }

        public string TokenCode { get; set; }

        public decimal Amount { get; set; }
    }
}
