using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Token;
using Nexus.Sdk.Token.KeyPairs;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;
using Nexus.Sdk.Token.Security;
using Nexus.Token.Stellar.Examples.Models;
using NJsonSchema;

namespace Nexus.Token.Stellar.Examples
{
    public class StellarExamples
    {
        private readonly ITokenServer _tokenServer;
        private readonly IEncrypter _encrypter;
        private readonly IDecrypter _decrypter;
        private readonly ILogger<StellarExamples> _logger;
        private readonly string _network;

        public StellarExamples(ITokenServer tokenServer, IEncrypter encrypter, IDecrypter decrypter, ILogger<StellarExamples> logger, StellarSettings stellarSettings)
        {
            _tokenServer = tokenServer;
            _encrypter = encrypter;
            _decrypter = decrypter;
            _logger = logger;

            if (stellarSettings.NetworkPassphrase is null)
            {
                throw new ArgumentNullException(stellarSettings.NetworkPassphrase);
            }

            _network = stellarSettings.NetworkPassphrase;
        }

        public async Task<string> CreateAccountAsync(string customerCode, string[]? allowedTokens = null)
        {
            var bankAccount = new CustomerBankAccountRequest[]
            {
                new ()
                {
                    IdentifiedBankAccountName = "BankAccountName",
                    BankAccountNumber = Guid.NewGuid().ToString(),
                    Bank = new BankRequest
                    {
                        BankBicCode = "BankBicCode",
                        BankName = "BankName",
                        BankCountryCode = "BankCountry",
                        BankCity = "BankCity",
                        BankIBANCode = "IbanCode",
                    }
                }
            };

            string customerIPAddress = "127.1.0.0";

            var request = new CreateCustomerRequestBuilder(customerCode, "Trusted", "EUR")
                .SetBankAccounts(bankAccount)
                .AddCustomProperty("FirstName", "Test")
                .Build();

            var customer = await _tokenServer.Customers.Create(request, customerIPAddress);

            var kp = StellarKeyPair.Generate();

            _logger.LogWarning("Generated new nexus account: {accountCode} use this code for most future operations", kp.GetAccountCode());
            _logger.LogWarning("This is its private key encrypted private key: {privateKey}. It can be stored in the database for example or on the users mobile phone",
                                kp.GetPrivateKey(_encrypter));

            if (allowedTokens != null)
            {
                var signableResponse = await _tokenServer.Accounts.CreateOnStellarAsync(customer.CustomerCode, kp.GetPublicKey(), allowedTokens);
                var signedResponse = kp.Sign(signableResponse, _network);
                await _tokenServer.Submit.OnStellarAsync(signedResponse);
            }
            else
            {
                await _tokenServer.Accounts.CreateOnStellarAsync(customer.CustomerCode, kp.GetPublicKey());
            }

            _logger.LogWarning("Customer and account successfully created");

            return kp.GetPrivateKey(_encrypter);
        }

        public async Task CreateAssetTokenAsync(string tokenCode, string tokenName)
        {
            var definition = StellarTokenDefinition.TokenizedAsset(tokenCode, tokenName);

            var response = await _tokenServer.Tokens.CreateOnStellar(definition);
            var token = response.Tokens.First();

            _logger.LogWarning("A new asset token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);
        }

        public async Task CreateAssetTokenMultipleAsync(IDictionary<string, string> tokens)
        {
            var definitions = tokens.Select(t => StellarTokenDefinition.TokenizedAsset(t.Key, t.Value));

            var response = await _tokenServer.Tokens.CreateOnStellar(definitions);

            foreach (var token in response.Tokens)
            {
                // Notices that all tokens are generated under the same issuer address
                _logger.LogWarning("New tokens were generated under the following issuer address: {issuerAddress}", token.IssuerAddress);
            }
        }

        public async Task CreateStableCoinTokenAsync(string tokenCode, string tokenName, string currencyCode, decimal rate)
        {
            var definition = StellarTokenDefinition.StableCoin(tokenCode, tokenName, currencyCode, rate);

            var response = await _tokenServer.Tokens.CreateOnStellar(definition);
            var token = response.Tokens.First();

            _logger.LogWarning("A new stable coin was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);
        }

        public async Task CreateAssetTokenWithTaxonomyAsync()
        {
            var schema = JsonSchema.FromType<Person>();
            await _tokenServer.Taxonomy.CreateSchema("PersonSchema", schema.ToJson(), "Person", "Schema of a Person");

            var taxonomy = new TaxonomyRequest("PersonSchema", "https://hanspeter.com");
            taxonomy.AddProperty("FirstName", "Hans");
            taxonomy.AddProperty("LastName", "Peter");

            var definition = StellarTokenDefinition.TokenizedAsset("HP", "Hans Peter");
            definition.SetTaxonomy(taxonomy);

            var tokenResponse = await _tokenServer.Tokens.CreateOnStellar(definition);
            var token = tokenResponse.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);

            var taxonomyResponse = await _tokenServer.Taxonomy.Get("HP");
            _logger.LogWarning("The hash of the Hans Peter token is: {hash}", taxonomyResponse.Hash);
            _logger.LogWarning("The properties of the Hans Peter token is: {validatedTaxonomy}", taxonomyResponse.ValidatedTaxonomy);
        }

        public async Task FundAccountAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var accountBalances = await _tokenServer.Accounts.GetBalances(kp.GetAccountCode());

            if (!accountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogWarning("Account is not connected to the token, so we need to connect it first");
                await ConnectTokensAsync(encryptedPrivateKey, new string[] { tokenCode });
            }

            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), tokenCode, amount);

            _logger.LogWarning("Funding successful!");
        }

        public async Task FundAccountMultipleAsync(string encryptedPrivateKey, IDictionary<string, decimal> fundings)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var tokenCodes = fundings.Select(kv => kv.Key);

            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(kp.GetAccountCode(), tokenCodes);
            var signedResponse = kp.Sign(signableResponse, _network);
            await _tokenServer.Submit.OnStellarAsync(signedResponse);

            var definitions = fundings.Select(kv => new FundingDefinition(kv.Key, kv.Value, null, null));
            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), definitions);

            _logger.LogWarning("Funding multiple successful!");
        }

        public async Task ConnectTokensAsync(string encryptedPrivateKey, string[] tokenCodes)
        {
            var keypair = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(keypair.GetAccountCode(), tokenCodes);
            var signedResponse = keypair.Sign(signableResponse, _network);

            await _tokenServer.Submit.OnStellarAsync(signedResponse);
        }

        public async Task PaymentAsync(string encryptedSenderPrivateKey, string encryptedReceiverPrivateKey, string tokenCode, decimal amount)
        {
            var sender = StellarKeyPair.FromPrivateKey(encryptedSenderPrivateKey, _decrypter);
            var receiver = StellarKeyPair.FromPrivateKey(encryptedReceiverPrivateKey, _decrypter);

            var receiverAccountBalances = await _tokenServer.Accounts.GetBalances(receiver.GetAccountCode());

            if (!receiverAccountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogWarning("Receiver account is not connected to the token, so we need to connect it first");
                await ConnectTokensAsync(encryptedReceiverPrivateKey, new string[] { tokenCode });
            }

            {
                var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(sender.GetPublicKey(), receiver.GetPublicKey(), tokenCode, amount);
                var signedResponse = sender.Sign(signableResponse, _network);
                await _tokenServer.Submit.OnStellarAsync(signedResponse);
            }

            _logger.LogWarning("Payment successful!");
        }

        public async Task MultiplePaymentsAsync(ExamplePayment[] payments)
        {
            var paymentDefinitions = payments
                .Select(p =>
                    {
                        var sender = StellarKeyPair.FromPrivateKey(p.SenderPrivateKey, _decrypter);
                        var receiver = StellarKeyPair.FromPrivateKey(p.ReceiverPrivateKey, _decrypter);

                        return new PaymentDefinition(sender.GetPublicKey(), receiver.GetPublicKey(), p.TokenCode, p.Amount, nonce: Guid.NewGuid().ToString());
                    })
                .ToArray();

            var signableResponse = await _tokenServer.Operations.CreatePaymentsAsync(paymentDefinitions);

            var signedResponses = payments.SelectMany(x =>
                {
                    var kp = StellarKeyPair.FromPrivateKey(x.SenderPrivateKey, _decrypter);

                    return kp.Sign(signableResponse, _network);
                })
                .ToList();

            if (signedResponses.Any())
            {
                await _tokenServer.Submit.OnStellarAsync(signedResponses);
                _logger.LogWarning("Payments successful!");
            }
            else
            {
                _logger.LogError("Envelope was not signed!");
            }
        }

        public async Task<PayoutResponse> PayoutAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var payoutResponse = await _tokenServer.Operations.SimulatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);
            _logger.LogWarning("Payout will be execute with the following amount: {amount}!", payoutResponse.Payout.ExecutedAmounts.TokenAmount);

            var signableResponse = await _tokenServer.Operations.CreatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);
            var signedResponse = kp.Sign(signableResponse, _network);
            await _tokenServer.Submit.OnStellarAsync(signedResponse);

            _logger.LogWarning("Payout successful!");

            return signableResponse.PayoutOperationResponse;
        }

        public async Task<OrderResponse> CreateBuyOrder(string encryptedPrivateKey, (string tokenCode, decimal amount) buying, (string tokenCode, decimal amount) selling)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var request = new OrderRequestBuilder(kp.GetAccountCode())
                .Buy(buying.amount, buying.tokenCode)
                .For(selling.amount, selling.tokenCode);

            var response = await _tokenServer.Orders.CreateOrder(request);
            var signedResponse = kp.Sign(response, _network);
            await _tokenServer.Submit.OnStellarAsync(signedResponse);

            var order = await _tokenServer.Orders.Get(response.CreatedOrder.Code);
            _logger.LogWarning($"Buy order successfully placed on the blockchain: {order.BlockchainCode ?? "Order was closed immidiatly so there is not blockchain code."}");

            return order;
        }

        public async Task<OrderResponse> CreateSellOrder(string encryptedPrivateKey, (string tokenCode, decimal amount) selling, (string tokenCode, decimal amount) buying)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var request = new OrderRequestBuilder(kp.GetAccountCode())
                .Sell(selling.amount, selling.tokenCode)
                .For(buying.amount, buying.tokenCode);

            var response = await _tokenServer.Orders.CreateOrder(request);
            var signedResponse = kp.Sign(response, _network);
            await _tokenServer.Submit.OnStellarAsync(signedResponse);

            var order = await _tokenServer.Orders.Get(response.CreatedOrder.Code);
            _logger.LogWarning($"Sell order successfully placed on the blockchain: {order.BlockchainCode ?? "Order was closed immidiatly so there is not blockchain code."}");

            return order;
        }

        public async Task CancelOrder(string encryptedPrivateKey, string orderCode)
        {
            var kp = StellarKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var signableResponse = await _tokenServer.Orders.CancelOrder(orderCode);
            var signedResponse = kp.Sign(signableResponse, _network);
            await _tokenServer.Submit.OnStellarAsync(signedResponse);

            _logger.LogWarning("Order successfully canceled!");
        }

        public async Task CreateAssetTokenWithTaxonomyAsync(string tokenCode, string tokenName)
        {
            var schema = JsonSchema.FromType<Person>();
            var schemaCode = Guid.NewGuid().ToString().Substring(0, 12);

            await _tokenServer.Taxonomy.CreateSchema(schemaCode, schema.ToJson(), "Person", "Schema of a Person");

            var url = $"https://{tokenCode}.com";
            var taxonomy = new TaxonomyRequest(schemaCode, url);
            taxonomy.AddProperty("Name", tokenName);
            taxonomy.AddProperty("Age", 25);

            var definition = StellarTokenDefinition.TokenizedAsset(tokenCode, tokenName);
            definition.SetTaxonomy(taxonomy);

            var tokenResponse = await _tokenServer.Tokens.CreateOnStellar(definition);
            var token = tokenResponse.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);

            var taxonomyResponse = await _tokenServer.Taxonomy.Get(tokenCode);
            _logger.LogWarning("The hash of the Hans Peter token is: {hash}", taxonomyResponse.Hash);
            _logger.LogWarning("The properties of the Hans Peter token is: {validatedTaxonomy}", taxonomyResponse.ValidatedTaxonomy);
        }

        public async Task<OrderResponse> GetOrderAsync(string orderCode)
        {
            return await _tokenServer.Orders.Get(orderCode);
        }

        public async Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode)
        {
            var fundingLimitsResponse = await _tokenServer.TokenLimits.GetFundingLimits(customerCode, tokenCode, "XLM");

            _logger.LogWarning("Returned token funding limits of the customer");
            return fundingLimitsResponse;
        }

        public async Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode)
        {
            var payoutLimitsResponse = await _tokenServer.TokenLimits.GetPayoutLimits(customerCode, tokenCode, "XLM");

            _logger.LogWarning("Returned token payout limits of the customer");
            return payoutLimitsResponse;
        }

        public async Task<TokenOperationResponse> UpdateOperationStatusAsync(string operationCode, string status, string? paymentReference = null)
        {
            _logger.LogWarning("Updating operation with status: {status} and payment reference: {paymentReference}", status, paymentReference);
            var tokenOperationResponse = await _tokenServer.Operations.UpdateOperationStatusAsync(operationCode, status, paymentReference: paymentReference);

            _logger.LogWarning("Operation update successful!");
            return tokenOperationResponse;
        }
    }
}
