# Token SDK Examples

This project contains examples on how to use the Nexus Token SDK.

- [AlgorandFlow](./AlgorandExample.cs): Examples of basic supported token flows that can be executed on Algorand blockchain. 
- [Program](./Program.cs): Example on how to setup the SDK for an executable project.
- [Startup](./Startup.cs): Example on how to setup the SDK for an MVC project.

## *Signing Process*
Some operations in Nexus require an account to authorize it. This process is know as `signing`. The operations that require this return a `SignableResponse`. This response is signed using the key pairs `.Sign(...)` method. This method returns a `SubmitRequest` that is submitted to Nexus.

**NOTE:** That any operation that requires signing will not appear on the blockchain until it is successfully submitted.

Algorand Example:
```csharp
var kp = AlgorandKeyPair.Load("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), tokenCode);
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnAlgorandAsync(signedResponse);
```

Stellar Example:
```csharp

var kp = StellarKeyPair.Load("private_key");

var signableResponse = await _tokenServer.Accounts.ConnectToTokenAsync(kp.GetAccountCode(), tokenCode);
var signedResponse = kp.Sign(signableResponse);

await _tokenServer.Submit.OnStellarAsync(signedResponse);
```