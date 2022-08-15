# Token SDK Examples

This project contains examples on how to use the Nexus Token SDK.

- [AlgorandFlow](./AlgorandExamples.cs): Examples of basic supported token flows that can be executed on Algorand blockchain. 
- [Program](./Program.cs): Example on how to setup the SDK for an executable project.
- [Startup](./Startup.cs): Example on how to setup the SDK for an MVC project.

## Flows

| # 	| **Flow Name**                	| **Description**                                                                                                                                                                           	|
|---	|------------------------------	|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| 0 	| Algorand Payment Flow        	| A payment flow between two customers. One token is created and funded to a customer in exchange for fiat. This customer sends the token to the other customer using a payment.            	|
| 1 	| Algorand Payout Flow         	| A payout flow for one customer. One token is created and funded to a customer for fiat. This customer waits for the value of the token to increase and later pays out the token for fiat. 	|
| 2 	| Algorand Token Taxonomy Flow 	| A flow to show how to add taxonomy to a token.                                                                                                                                            	|

## *Signing Process*
Some operations in Nexus require an account(s) to authorize them. This process is know as `signing`. The operations that require this authorization return a `SignableResponse`. This response needs to be signed using the key pairs `.Sign(...)` method. Upon successful signing, a `SubmitRequest` is returned that must be submitted to Nexus.

**NOTE:** That any operation that requires signing will not appear on the blockchain until it is successfully submitted.

Here is an example if only a single authorization is required from the same key pair e.g. `ConnectToTokenAsync`:

Algorand Example:
```csharp
var kp = AlgorandKeyPair.FromPrivateKey("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), "Gold");

// Sign using a single account
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
```

Here is an example if multiple authorizations are required from different key pairs e.g. `Payments`. Two different parties would like to exchange 5 gold for 10 silver. This means both need to authorize this operation with their signature:

Algorand Example:
```csharp
var sender = AlgorandKeyPair.FromPrivateKey("private_key");
var receiver = AlgorandKeyPair.FromPrivateKey("private_key");

var payments = new List<PaymentDefinition>();

payments.Add(new PaymentDefinition(sender.GetPublicKey(), receiver.GetPublicKey(), "Gold", 5));

payments.Add(new PaymentDefinition(receiver.GetPublicKey(), sender.GetPublicKey(), "Silver", 10));

var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(payments);

// Sign using multiple accounts
var senderSignedResponse = sender.Sign(signableResponse);
await _tokenServer.Submit.OnAlgorandAsync(senderSignedResponse);

var receiverSignedResponse = receiver.Sign(signableResponse);
await _tokenServer.Submit.OnAlgorandAsync(receiverSignedResponse);
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
  }
}
```