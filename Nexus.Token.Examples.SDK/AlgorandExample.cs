using Microsoft.Extensions.Logging;
using Nexus.Token.Examples.SDK.Models;
using Nexus.Token.SDK;
using Nexus.Token.SDK.KeyPairs;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Security;
using NJsonSchema;

namespace Nexus.Token.Examples.SDK
{
    public class AlgorandExample
    {
        private readonly ITokenServer _tokenServer;
        private readonly IEncrypter _encrypter;
        private readonly IDecrypter _decrypter;
        private readonly ILogger<AlgorandExample> _logger;

        public AlgorandExample(ITokenServer tokenServer, IEncrypter encrypter, IDecrypter decrypter, ILogger<AlgorandExample> logger)
        {
            _tokenServer = tokenServer;
            _encrypter = encrypter;
            _decrypter = decrypter;
            _logger = logger;
        }

        public async Task<string> CreateAccountFlowAsync(string customerCode)
        {
                var customer = await _tokenServer.Customers.Create(customerCode, "Trusted", "EUR");

                var senderKeyPair = AlgorandKeyPair.Generate();

                _logger.LogWarning("Generated new nexus account: {accountCode} use this code for most future operations", senderKeyPair.GetAccountCode());
                _logger.LogWarning("This is its private key encrypted private key: {privateKey}. It can be stored in the database for example or on the users mobile phone",
                                    senderKeyPair.GetPrivateKey(_encrypter));

                await _tokenServer.Accounts.CreateOnAlgorandAsync("TEST_CUSTOMER_5", senderKeyPair.GetPublicKey());

                _logger.LogInformation("Customer and account successfully created");

                return senderKeyPair.GetPrivateKey(_encrypter);
        }

        public async Task CreateAssetTokenFlowAsync(string tokenCode, string tokenName)
        {
            var definition = AlgorandTokenDefinition.TokenizedAsset(tokenCode, tokenName, 1000, 0);

            var response = await _tokenServer.Tokens.CreateOnAlgorand(definition);
            var token = response.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);
        }

        public async Task CreateAssetTokenWithTaxonomyFlowAsync()
        {
            var schema = JsonSchema.FromType<Person>();
            await _tokenServer.Taxonomy.CreateSchema("PersonSchema", schema.ToJson(), "Person", "Schema of a Person");

            var taxonomy = new TaxonomyRequest("PersonSchema", "https://hanspeter.com");
            taxonomy.AddProperty("FirstName", "Hans");
            taxonomy.AddProperty("LastName", "Peter");
            taxonomy.AddProperty("Gender", (int)Gender.Male);

            var definition = AlgorandTokenDefinition.TokenizedAsset("HP", "Hans Peter", 1000, 0);
            definition.SetTaxonomy(taxonomy);

            var tokenResponse = await _tokenServer.Tokens.CreateOnAlgorand(definition);
            var token = tokenResponse.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);

            var taxonomyResponse = await _tokenServer.Taxonomy.Get("HP");
            _logger.LogWarning("The hash of the Hans Peter token is: {issuerAddress}", taxonomyResponse.Hash);
            _logger.LogWarning("The properties of the Hans Peter token is: {validatedTaxonomy}", taxonomyResponse.ValidatedTaxonomy);
        }

        public async Task FundAccountFlowAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var accountBalances = await _tokenServer.Accounts.GetBalances(kp.GetAccountCode());

            if (!accountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogInformation("Account is not connected to the token, so we need to connect it first");

                var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), tokenCode);
                var signedResponse = kp.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), tokenCode, amount);
        }

        public async Task PaymentFlowAsync(string encryptedSenderPrivateKey, string encryptedReceiverPrivateKey, string tokenCode, decimal amount)
        {
            var sender = AlgorandKeyPair.FromPrivateKey(encryptedSenderPrivateKey, _decrypter);
            var receiver = AlgorandKeyPair.FromPrivateKey(encryptedReceiverPrivateKey, _decrypter);

            var receiverAccountBalances = await _tokenServer.Accounts.GetBalances(receiver.GetAccountCode());

            if (!receiverAccountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogInformation("Receiver account is not connected to the token, so we need to connect it first");

                var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(receiver.GetAccountCode(), tokenCode);
                var signedResponse = receiver.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            {
                var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(sender.GetPublicKey(), receiver.GetPublicKey(), tokenCode, amount);
                var signedResponse = sender.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }
        }

        public async Task PayoutFlowAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var signableResponse = await _tokenServer.Operations.CreatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);
            var signedResponse = kp.Sign(signableResponse);
            await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
        }
    }
}
