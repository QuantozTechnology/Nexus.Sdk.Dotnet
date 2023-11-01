# Token Sdk Examples

This project contains examples on how to use the Nexus Token Sdk.

- [AlgorandFlow](./AlgorandExamples.cs): Examples of basic supported token flows that can be executed on Algorand blockchain. 
- [Program](./Program.cs): Example on how to setup the sdk for an executable project.
- [Startup](./Startup.cs): Example on how to setup the sdk for an MVC project.

## Flows

| # 	| **Flow Name**                	| **Description**                                                                                                                                                                           	|
|---	|------------------------------	|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| 0 	| Algorand Payment Flow        	| A payment flow between two customers. One token is created and funded to a customer in exchange for fiat. This customer sends the token to the other customer using a payment.            	|
| 1 	| Algorand Payout Flow         	| A payout flow for one customer. One token is created and funded to a customer for fiat. This customer waits for the value of the token to increase and later pays out the token for fiat. 	|
| 2 	| Algorand Token Taxonomy Flow 	| A flow to show how to add taxonomy to a token.                                                                                                                                            	|
| 3 	| Algorand Multiple Operations Flow     	| Submitting to the blockchain on Algorand can take up to ~8 seconds. If a customer would like to be funded with multiple tokens this could take a while if funded individually. To speed this up, Nexus supports funding multiple tokens at the same time. This flow demonstrates this and some of the other functionalities that can be executed at the same time.             |
| 4 	| Algorand Token Limits Flow     | This flow demonstrates the limits functionality during various payments operations. It lists the total and remaining funding and payout limits for a customer.                               |

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

### Background Signing
By providing a `callback` url during the Signing process, the resulting `SubmitRequest` will be processed through a background service. What this means is that the signed result will be validated. If found valid, it will be processed as soon as all the required signatures have been added. The eventual result will be sent to the `callbackUrl`. This flow is recommended for payment and payout processes.

Algorand Example:
```csharp
var sender = AlgorandKeyPair.FromPrivateKey("private_key");
var receiver = AlgorandKeyPair.FromPrivateKey("private_key");

var payments = new List<PaymentDefinition>();

payments.Add(new PaymentDefinition(sender.GetPublicKey(), receiver.GetPublicKey(), "Gold", 5));

payments.Add(new PaymentDefinition(receiver.GetPublicKey(), sender.GetPublicKey(), "Silver", 10));

var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(payments);

// Sign using multiple accounts
var senderSignedResponse = sender.Sign(signableResponse, callbackUrl: "your-callback-url");
await _tokenServer.Submit.OnAlgorandAsync(senderSignedResponse);

var receiverSignedResponse = receiver.Sign(signableResponse, callbackUrl: "your-callback-url");
await _tokenServer.Submit.OnAlgorandAsync(receiverSignedResponse);
```

Callback Example:
```json
{
  "Type": "TokenPayment",
  "PaymentCode": "04D4B45F9EE742E8A5E0F6E7A9FBB9BB",
  "PaymentStatus": "SubmissionCompleted"
}
```

## Setup

To run this sample code you need to connect it to your test environment using the `ClientId`, `ClientSecret` and `PaymentMethod` configured in the `appsettings.json`

```json
{
  "NexusOptions": {
    "ApiUrl": "https://testapi.quantoz.com",
    "PaymentMethodOptions": {
      "Funding": "",
      "Payout": ""
    },
    "AuthProviderOptions": {
      "IdentityUrl": "https://testidentity.quantoz.com",
      "ClientId": "",
      "ClientSecret": ""
    }
  }
}
```