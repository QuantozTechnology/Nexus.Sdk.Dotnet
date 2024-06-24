using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses
{
    public record PaymentMethodsResponse
    {
        [JsonConstructor]
        public PaymentMethodsResponse(string code, string name, string cryptoCode, string currencyCode, PaymentMethodFees fees, IDictionary<string, string> data)
        {
            Code = code;
            Name = name;
            CryptoCode = cryptoCode;
            CurrencyCode = currencyCode;
            Fees = fees;
            Data = data;
        }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cryptoCode")]
        public string CryptoCode { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("fees")]
        public PaymentMethodFees Fees { get; set; }

        [JsonPropertyName("data")]
        public IDictionary<string, string> Data { get; set; }
    }

    public record PaymentMethodFees
    {
        [JsonConstructor]
        public PaymentMethodFees(BankFees bankFees, ServiceFees serviceFees)
        {
            BankFees = bankFees;
            ServiceFees = serviceFees;
        }

        [JsonPropertyName("bank")]
        public BankFees BankFees { get; set; }

        [JsonPropertyName("service")]
        public ServiceFees ServiceFees { get; set; }
    }

    public record BankFees
    {
        [JsonConstructor]
        public BankFees(double fixedFees, double relativeFees)
        {
            FixedFees = fixedFees;
            RelativeFees = relativeFees;
        }

        [JsonPropertyName("fixed")]
        public double FixedFees { get; set; }

        [JsonPropertyName("relative")]
        public double RelativeFees { get; set; }
    }

    public record ServiceFees
    {
        [JsonConstructor]
        public ServiceFees(double relativeFees)
        {
            RelativeFees = relativeFees;
        }

        [JsonPropertyName("relative")]
        public double RelativeFees { get; set; }
    }
}
