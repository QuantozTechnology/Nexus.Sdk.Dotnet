namespace Nexus.Sdk.Shared.Requests
{
    public class CustomerRequestBuilder<T> where T : CustomerRequest, new()
    {
        protected readonly T _request;

        public CustomerRequestBuilder()
        {
            _request = new();
        }

        protected CustomerRequestBuilder<T> SetCustomerCode(string customerCode)
        {
            _request.CustomerCode = customerCode;
            return this;
        }

        public CustomerRequestBuilder<T> SetEmail(string email)
        {
            _request.Email = email;
            return this;
        }

        public CustomerRequestBuilder<T> SetStatus(CustomerStatus status)
        {
            _request.Status = status.ToString();
            return this;
        }

        public CustomerRequestBuilder<T> SetFirstName(string firstName)
        {
            _request.FirstName = firstName;
            return this;
        }

        public CustomerRequestBuilder<T> SetLastName(string lastName)
        {
            _request.LastName = lastName;
            return this;
        }

        public CustomerRequestBuilder<T> SetDateOfBirth(string dateOfBirth)
        {
            _request.DateOfBirth = dateOfBirth;
            return this;
        }

        public CustomerRequestBuilder<T> SetPhone(string phone)
        {
            _request.Phone = phone;
            return this;
        }

        public CustomerRequestBuilder<T> SetCompanyName(string companyName)
        {
            _request.CompanyName = companyName;
            return this;
        }

        public CustomerRequestBuilder<T> SetRiskQualification(string riskQualification)
        {
            _request.RiskQualification = riskQualification;
            return this;
        }

        public CustomerRequestBuilder<T> SetCustomData(IDictionary<string, string> data)
        {
            _request.Data = data;
            return this;
        }

        public CustomerRequestBuilder<T> AddCustomProperty(string key, string value)
        {
            var data = new Dictionary<string, string>
            {
                [key] = value
            };

            _request.Data = data;
            return this;
        }

        public CustomerRequestBuilder<T> SetCountry(string countryCode)
        {
            _request.CountryCode = countryCode;
            return this;
        }

        public T Build()
        {
            return _request;
        }
    }

    public class CreateCustomerRequestBuilder : CustomerRequestBuilder<CreateCustomerRequest>
    {
        public CreateCustomerRequestBuilder SetTrustLevel(string trustLevel)
        {
            _request.TrustLevel = trustLevel;
            return this;
        }

        public CreateCustomerRequestBuilder SetCurrencyCode(string currencyCode)
        {
            _request.CurrencyCode = currencyCode;
            return this;
        }

        public CreateCustomerRequestBuilder SetExternalCustomerCode(string externalCustomerCode)
        {
            _request.ExternalCustomerCode = externalCustomerCode;
            return this;
        }

        public CreateCustomerRequestBuilder SetIsBusiness(bool isBusiness)
        {
            _request.IsBusiness = isBusiness;
            return this;
        }

        public CreateCustomerRequestBuilder SetBankAccounts(CustomerBankAccountRequest[] bankAccounts)
        {
            _request.BankAccounts = bankAccounts;
            return this;
        }

        public CreateCustomerRequestBuilder(string customerCode, string trustlevel, string currencyCode)
        {
            SetCustomerCode(customerCode);
            SetTrustLevel(trustlevel);
            SetCurrencyCode(currencyCode);
        }
    }

    public class UpdateCustomerRequestBuilder : CustomerRequestBuilder<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestBuilder(string customerCode)
        {
            SetCustomerCode(customerCode);
        }

        public UpdateCustomerRequestBuilder SetTrustLevel(string trustLevel)
        {
            _request.TrustLevel = trustLevel;
            return this;
        }

        public UpdateCustomerRequestBuilder SetReason(string reason)
        {
            _request.Reason = reason;
            return this;
        }

        public UpdateCustomerRequestBuilder SetBankAccounts(UpdateCustomerBankAccountRequest[] bankAccounts)
        {
            _request.BankAccounts = bankAccounts;
            return this;
        }
    }
}
