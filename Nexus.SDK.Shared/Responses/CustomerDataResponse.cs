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
        public string CustomerCode { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
    }
}
