# Token SDK Examples

This project contains examples on how to use the Nexus Token SDK.

- [AlgorandFlow](./AlgorandExample.cs): Examples of basic supported token flows that can be executed on Algorand blockchain. 
- [Program](./Program.cs): Example on how to setup the SDK for an executable project.
- [Startup](./Startup.cs): Example on how to setup the SDK for an MVC project.

## *Signing Process*
Some operations in Nexus require an account(s) to authorize them. This process is know as `signing`. The operations that require this authorization return a `SignableResponse`. This response needs to be signed using the key pairs `.Sign(...)` method. Upon successful signing, a `SubmitRequest` is returned that must be submitted to Nexus.

**NOTE:** That any operation that requires signing will not appear on the blockchain until it is successfully submitted.


Here is an example if only a single authorization is required from the same key pair e.g. `ConnectToTokenAsync`:

Algorand Example:
```csharp
var kp = AlgorandKeyPair.Load("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), "Gold");

// Sign using a single account
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
```

Stellar Example:
```csharp
var kp = StellarKeyPair.Load("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), "Gold");

// Sign using a single account
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnStellarAsync(signedResponse);
```

Here is an example if multiple authorizations are required from different key pairs e.g. `Payments`. Two different parties would like to exchange 5 gold for 10 silver. This means both need to authorize this operation with their signature:

Algorand Example:
```csharp
var sender = AlgorandKeyPair.Load("private_key");
var receiver = AlgorandKeyPair.Load("private_key");

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

Stellar Example:
```csharp
var sender = StellarKeyPair.Load("private_key");
var receiver = StellarKeyPair.Load("private_key");

var payments = new List<PaymentDefinition>();

payments.Add(new PaymentDefinition(sender.GetPublicKey(), receiver.GetPublicKey(), "Gold", 5));

payments.Add(new PaymentDefinition(receiver.GetPublicKey(), sender.GetPublicKey(), "Silver", 10));

var signableResponse = await _tokenServer.Operations.CreatePaymentAsync(payments);

// Sign using multiple accounts
var senderSignedResponse = sender.Sign(signableResponse);
var receiverSignedResponse = receiver.Sign(senderSignedResponse);

await _tokenServer.Submit.OnStellarAsync(signedResponse);
```
