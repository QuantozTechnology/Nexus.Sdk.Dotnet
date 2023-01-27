using Nexus.SDK.Shared.Requests;
using Nexus.SDK.Shared.Responses;

namespace Nexus.SDK.Shared.Requests
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

        protected CustomerRequestBuilder<T> SetTrustlevel(string trustlevel)
        {
            _request.TrustLevel = trustlevel;
            return this;
        }

        protected CustomerRequestBuilder<T> SetCurrencyCode(string currencyCode)
        {
            _request.CurrencyCode = currencyCode;
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

        public CustomerRequestBuilder<T> SetBankAccounts(CustomerBankAccountRequest[] bankAccounts)
        {
            _request.BankAccounts = bankAccounts.ToList();
            return this;
        }

        public CustomerRequestBuilder<T> AddBankAccount(CustomerBankAccountRequest request)
        {
            _request.BankAccounts.Add(request);
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

        public CustomerRequestBuilder<T> SetExternalReference(string externalReference)
        {
            _request.ExternalCustomerCode = externalReference;
            return this;
        }

        public CustomerRequestBuilder<T> SetBusiness(bool isBusiness)
        {
            _request.IsBusiness = isBusiness;
            return this;
        }

        public T Build()
        {
            return _request;
        }
    }

    public class CreateCustomerRequestBuilder : CustomerRequestBuilder<CreateCustomerRequest>
    {
        public CreateCustomerRequestBuilder(string customerCode, string trustlevel, string currencyCode)
        {
            SetCustomerCode(customerCode);
            SetTrustlevel(trustlevel);
            SetCurrencyCode(currencyCode);
        }
    }

    public class UpdateCustomerRequestBuilder : CustomerRequestBuilder<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestBuilder(string customerCode, string? reason = null)
        {
            SetCustomerCode(customerCode);
            _request.Reason = reason;
        }

    }
}
