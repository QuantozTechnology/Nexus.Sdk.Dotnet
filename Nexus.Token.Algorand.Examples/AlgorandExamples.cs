using Microsoft.Extensions.Logging;
using Nexus.SDK.Shared.Requests;
using Nexus.Token.Examples.SDK.Models;
using Nexus.Token.SDK;
using Nexus.Token.SDK.KeyPairs;
using Nexus.Token.SDK.Requests;
using Nexus.Token.SDK.Security;
using NJsonSchema;

namespace Nexus.Token.Examples.SDK
{
    public class AlgorandExamples
    {
        private readonly ITokenServer _tokenServer;
        private readonly IEncrypter _encrypter;
        private readonly IDecrypter _decrypter;
        private readonly ILogger<AlgorandExamples> _logger;

        public AlgorandExamples(ITokenServer tokenServer, IEncrypter encrypter, IDecrypter decrypter, ILogger<AlgorandExamples> logger)
        {
            _tokenServer = tokenServer;
            _encrypter = encrypter;
            _decrypter = decrypter;
            _logger = logger;
        }

        public async Task<string> CreateAccountAsync(string customerCode)
        {
            var request = new CustomerRequestBuilder(customerCode, "Trusted", "EUR", "NL", "MOCK_EXTERNAL_CODE").Build();
            var customer = await _tokenServer.Customers.Create(request);

            var senderKeyPair = AlgorandKeyPair.Generate();

            _logger.LogWarning("Generated new nexus account for {customerCode}: {accountCode} use this code for most future operations", customer.CustomerCode, senderKeyPair.GetAccountCode());
            _logger.LogWarning("This is its private key encrypted private key: {privateKey}. It can be stored in the database for example or on the users mobile phone",
                                senderKeyPair.GetPrivateKey(_encrypter));

            await _tokenServer.Accounts.CreateOnAlgorandAsync(customer.CustomerCode, senderKeyPair.GetPublicKey());

            _logger.LogWarning("Customer and account successfully created");

            return senderKeyPair.GetPrivateKey(_encrypter);
        }

        public async Task CreateAssetTokenAsync(string tokenCode, string tokenName)
        {
            var definition = AlgorandTokenDefinition.TokenizedAsset(tokenCode, tokenName, 1000, 0);

            var response = await _tokenServer.Tokens.CreateOnAlgorand(definition);
            var token = response.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);
        }

        public async Task CreateAssetTokenMultipleAsync(IDictionary<string, string> tokens)
        {
            var definitions = tokens.Select(t => AlgorandTokenDefinition.TokenizedAsset(t.Key, t.Value, 1000, 0));

            var response = await _tokenServer.Tokens.CreateOnAlgorand(definitions);

            foreach(var token in response.Tokens)
            {
                // Notices that all tokens are generated under the same issuer address
                _logger.LogWarning("New tokens were generated under the following issuer address: {issuerAddress}", token.IssuerAddress);
            }
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

            var definition = AlgorandTokenDefinition.TokenizedAsset(tokenCode, tokenName, 1000, 0);
            definition.SetTaxonomy(taxonomy);

            var tokenResponse = await _tokenServer.Tokens.CreateOnAlgorand(definition);
            var token = tokenResponse.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);

            var taxonomyResponse = await _tokenServer.Taxonomy.Get(tokenCode);
            _logger.LogWarning("The hash of the Hans Peter token is: {hash}", taxonomyResponse.Hash);
            _logger.LogWarning("The properties of the Hans Peter token is: {validatedTaxonomy}", taxonomyResponse.ValidatedTaxonomy);
        }

        public async Task FundAccountAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var accountBalances = await _tokenServer.Accounts.GetBalances(kp.GetAccountCode());

            if (!accountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogWarning("Account is not connected to the token, so we need to connect it first");

                var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), tokenCode);
                var signedResponse = kp.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), tokenCode, amount);
            _logger.LogWarning("Funding successful!");
        }

        public async Task FundAccountMultipleAsync(string encryptedPrivateKey, IDictionary<string, decimal> fundings)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var tokenCodes = fundings.Select(kv => kv.Key);
            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(kp.GetAccountCode(), tokenCodes);
            var signedResponse = kp.Sign(signableResponse);
            await _tokenServer.Submit.OnAlgorandAsync(signedResponse);

            var definitions = fundings.Select(kv => new FundingDefinition(kv.Key, kv.Value));
            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), definitions);

            _logger.LogWarning("Funding multiple successful!");
        }

        public async Task ConnectTokensFlowAsync(string encryptedPrivateKey, string[] tokenCodes)
        {
            var keypair = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);
            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(keypair.GetAccountCode(), tokenCodes);
            var signedResponse = keypair.Sign(signableResponse);

            _ = _tokenServer.Submit.OnAlgorandAsync(signedResponse);
        }

        public async Task PaymentAsync(string encryptedSenderPrivateKey, string encryptedReceiverPrivateKey, string tokenCode, decimal amount)
        {
            var sender = AlgorandKeyPair.FromPrivateKey(encryptedSenderPrivateKey, _decrypter);
            var receiver = AlgorandKeyPair.FromPrivateKey(encryptedReceiverPrivateKey, _decrypter);

            var receiverAccountBalances = await _tokenServer.Accounts.GetBalances(receiver.GetAccountCode());

            if (!receiverAccountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogWarning("Receiver account is not connected to the token, so we need to connect it first");

                var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(receiver.GetAccountCode(), tokenCode);
                var signedResponse = receiver.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            {
                var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(sender.GetPublicKey(), receiver.GetPublicKey(), tokenCode, amount);
                var signedResponse = sender.Sign(signableResponse);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            _logger.LogWarning("Payment successful!");

        }

        public async Task PayoutAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var signableResponse = await _tokenServer.Operations.CreatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);
            var signedResponse = kp.Sign(signableResponse);
            await _tokenServer.Submit.OnAlgorandAsync(signedResponse);

            _logger.LogWarning("Payout successful!");
        }
    }
}
