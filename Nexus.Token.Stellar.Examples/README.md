# Token SDK Examples

This project contains examples on how to use the Nexus Token SDK.

- [Stellar Examples](./StellarExamples.cs): Examples of basic supported token functionality that can be executed on Stellar blockchain. 
- [Program](./Program.cs): Example on how to setup the SDK for an executable project.

## Flows

| # 	| **Flow Name**               	| **Description**                                                                                                                                                                                                                                                                                                                                                                                 	|
|---	|-----------------------------	|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| 0 	| Stellar Payment Flow        	| A payment flow between two customers. One token is created and funded to a customer in exchange for fiat. This customer sends the token to the other customer using a payment.                                                                                                                                                                                                                  	|
| 1 	| Stellar Payout Flow         	| A payout flow for one customer. One token is created and funded to a customer for fiat. This customer waits for the value of the token to increase and later pays out the token for fiat.                                                                                                                                                                                                       	|
| 2 	| Stellar Token Taxonomy Flow 	| A flow to show how to add taxonomy to a token.                                                                                                                                                                                                                                                                                                                                                  	|
| 3 	| Stellar Orderbook Flow      	| An orderbook flow between two customers. Two tokens are created, one asset and one stablecoin. Each customer is funded with one token in exchange for fiat. One customer places a SELL order to sell their asset for stablecoin and the other customer places a BUY order to buy asset for stablecoin. These two orders are matched on the blockchain and both customer's receive their tokens. 	|
| 4 	| Stellar Stablecoin Flow     	| A flow between a customer and a client. Two tokens are created, one asset and one stablecoin. The customer  is funded with stablecoin in exchange for fiat. The customer can then use their stablecoin to purchase assets from the client. This purchase happens using two atomic payments.                                                                                                     	|
| 5 	| Stellar Multiple Operations Flow     	| Submitting to the blockchain on Stellar can take up to ~5 seconds. If a customer would like to be funded with multiple tokens this could take a while if funded individually. To speed this up, Nexus supports funding multiple tokens at the same time. This flow demonstrates this and some of the other functionalities that can be executed at the same time.                                                                               |
## *Signing Process*
Some operations in Nexus require an account(s) to authorize them. This process is know as `signing`. The operations that require this authorization return a `SignableResponse`. This response needs to be signed using the key pairs `.Sign(...)` method. Upon successful signing, a `SubmitRequest` is returned that must be submitted to Nexus.

**NOTE:** That any operation that requires signing will not appear on the blockchain until it is successfully submitted.

Here is an example if only a single authorization is required from the same key pair e.g. `ConnectToTokenAsync`:

Stellar Example:
```csharp
var kp = StellarKeyPair.FromPrivateKey("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), "Gold");

// Sign using a single account
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnStellarAsync(signedResponse);
```

Here is an example if multiple authorizations are required from different key pairs e.g. `Payments`. Two different parties would like to exchange 5 gold for 10 silver. This means both need to authorize this operation with their signature:

Stellar Example:
```csharp
var sender = StellarKeyPair.FromPrivateKey("private_key");
var receiver = StellarKeyPair.FromPrivateKey("private_key");

var payments = new List<PaymentDefinition>();

payments.Add(new PaymentDefinition(sender.GetPublicKey(), receiver.GetPublicKey(), "Gold", 5));

payments.Add(new PaymentDefinition(receiver.GetPublicKey(), sender.GetPublicKey(), "Silver", 10));

var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(payments);

// Sign using multiple accounts
var senderSignedResponse = sender.Sign(signableResponse);
var receiverSignedResponse = receiver.Sign(senderSignedResponse);

await _tokenServer.Submit.OnStellarAsync(signedResponse);
```

## Setup

To run this sample code you need to connect it to your test environment using the `ClientId`, `ClientSecret` and `xPaymentMethod` configured in the `appsettings.json`

```json
{
  "Settings": {
    "NexusApiUrl": "https://testapi.quantoznexus.com",
    "NexusIdentityUrl": "https://testidentity.quantoznexus.com",
    "ClientId": "",
    "ClientSecret": "",
    "FundingPaymentMethod": "",
    "PayoutPaymentMethod": "",
    "NetworkPassphrase": "Public Global Stellar Network ; September 2015"
  }
}
```