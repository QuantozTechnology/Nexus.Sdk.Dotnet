using Microsoft.Extensions.Logging;
using Nexus.Sdk.Shared.Requests;
using Nexus.Sdk.Token;
using Nexus.Sdk.Token.KeyPairs;
using Nexus.Sdk.Token.Requests;
using Nexus.Sdk.Token.Responses;
using Nexus.Sdk.Token.Security;
using Nexus.Token.Algorand.Examples.Models;
using NJsonSchema;

namespace Nexus.Token.Algorand.Examples
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
            var request = new CreateCustomerRequestBuilder(customerCode, "Trusted", "EUR")
                .Build();

            string customerIPAddress = null;

            var customer = await _tokenServer.Customers.Create(request, customerIPAddress);

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

            foreach (var token in response.Tokens)
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

        public async Task CreateAssetTokenWithSetHashForTaxonomy(string tokenCode, string tokenName)
        {
            var url = $"https://{tokenCode}.com";
            var hash = "341ed582360e949617b9f2107d881e8fdd3c99f22a4d1a7489cbc70382e2c2f5";
            var taxonomy = new TaxonomyRequest(url, hash);

            var definition = AlgorandTokenDefinition.TokenizedAsset(tokenCode, tokenName, 1000, 0);
            definition.SetTaxonomy(taxonomy);

            var tokenResponse = await _tokenServer.Tokens.CreateOnAlgorand(definition);
            var token = tokenResponse.Tokens.First();

            _logger.LogWarning("A new token was generated with the following issuer address: {issuerAddress}", token.IssuerAddress);

            var taxonomyResponse = await _tokenServer.Taxonomy.Get(tokenCode);
            _logger.LogWarning("The hash of the Hans Peter token is the value of the has that was provided: {hash}", taxonomyResponse.Hash);
        }

        public async Task FundAccountAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var accountBalances = await _tokenServer.Accounts.GetBalances(kp.GetAccountCode());

            if (!accountBalances.IsConnectedToToken(tokenCode))
            {
                _logger.LogWarning("Account is not connected to the token, so we need to connect it first");

                var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), tokenCode);
                var signedResponse = kp.Sign(signableResponse, true);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse, false);

                _logger.LogInformation("Successfully submitted the Account Connect request.");
                _logger.LogInformation("Waiting for completion...");

                // wait to be connected
                var result = await _tokenServer.Submit.WaitForCompletionAsync(signableResponse.BlockchainResponse.Code);

                if (result)
                {
                    _logger.LogInformation("Account successfully opted in");
                }
                else
                {
                    _logger.LogWarning("Account failed to opt in");
                }
            }

            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), tokenCode, amount);
            _logger.LogWarning("Funding successful!");
        }

        public async Task FundAccountMultipleAsync(string encryptedPrivateKey, IDictionary<string, decimal> fundings)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var tokenCodes = fundings.Select(kv => kv.Key);
            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(kp.GetAccountCode(), tokenCodes);
            var signedResponse = kp.Sign(signableResponse, true);
            await _tokenServer.Submit.OnAlgorandAsync(signedResponse);

            var definitions = fundings.Select(kv => new FundingDefinition(kv.Key, kv.Value, null));
            await _tokenServer.Operations.CreateFundingAsync(kp.GetAccountCode(), definitions);

            _logger.LogWarning("Funding multiple successful!");
        }

        public async Task ConnectTokensFlowAsync(string encryptedPrivateKey, string[] tokenCodes)
        {
            var keypair = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var signableResponse = await _tokenServer.Accounts.ConnectToTokensAsync(keypair.GetAccountCode(), tokenCodes);
            var signedResponse = keypair.Sign(signableResponse, true);

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
                var signedResponse = receiver.Sign(signableResponse, true);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);

                // wait to be connected
                var result = await _tokenServer.Submit.WaitForCompletionAsync(signableResponse.BlockchainResponse.Code);

                if (result)
                {
                    _logger.LogInformation("Account successfully opted in");
                }
                else
                {
                    _logger.LogWarning("Account failed to opt in");
                }
            }

            {
                var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(sender.GetPublicKey(), receiver.GetPublicKey(), tokenCode, amount, "memo", "message", "ALGO");
                var signedResponse = sender.Sign(signableResponse, true);
                await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
            }

            _logger.LogWarning("Payment successful!");

        }

        public async Task<PayoutResponse> PayoutAsync(string encryptedPrivateKey, string tokenCode, decimal amount)
        {
            var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);

            var payoutResponse = await _tokenServer.Operations.SimulatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);

            _logger.LogWarning("Payout will be execute with the following amount: {amount}!", payoutResponse.Payout.ExecutedAmounts.TokenAmount);

            var signableResponse = await _tokenServer.Operations.CreatePayoutAsync(kp.GetAccountCode(), tokenCode, amount);
            var signedResponse = kp.Sign(signableResponse, true);
            await _tokenServer.Submit.OnAlgorandAsync(signedResponse);

            _logger.LogWarning("Payout successful!");

            return signableResponse.PayoutOperationResponse;
        }

        public async Task<TokenLimitsResponse> GetTokenFundingLimits(string customerCode, string tokenCode)
        {
            var fundingLimitsResponse = await _tokenServer.TokenLimits.GetFundingLimits(customerCode, tokenCode);

            _logger.LogWarning("Returned token funding limits of the customer");
            return fundingLimitsResponse;
        }

        public async Task<TokenLimitsResponse> GetTokenPayoutLimits(string customerCode, string tokenCode)
        {
            var payoutLimitsResponse = await _tokenServer.TokenLimits.GetPayoutLimits(customerCode, tokenCode);

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