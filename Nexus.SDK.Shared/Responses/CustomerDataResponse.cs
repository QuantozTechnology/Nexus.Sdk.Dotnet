using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses
{
    public class CustomerDataResponse
    {
        [JsonConstructor]
        public CustomerDataResponse(string customerCode, string firstName, string lastName, string dateOfBirth, string countryName, string address, string city, string state, string phone, string companyName)
        {
            CustomerCode = customerCode;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            CountryName = countryName;
            Address = address;
            City = city;
            State = state;
            Phone = phone;
            CompanyName = companyName;
        }

        [JsonPropertyName("customerCode")]
        public string CustomerCode { get; private set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; private set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; private set; }

        [JsonPropertyName("dateOfBirth")]
        public string? DateOfBirth { get; private set; }

        [JsonPropertyName("countryName")]
        public string? CountryName { get; private set; }

        [JsonPropertyName("address")]
        public string? Address { get; private set; }

        [JsonPropertyName("city")]
        public string? City { get; private set; }

        [JsonPropertyName("state")]
        public string? State { get; private set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; private set; }

        [JsonPropertyName("companyName")]
        public string? CompanyName { get; private set; }
    }
}
