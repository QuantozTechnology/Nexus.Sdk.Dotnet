# Nexus Token SDK

This project provides the following functionalities to interact with your Nexus Token environments

| Functionality                    	| Algorand 	| Stellar 	|
|----------------------------------	|----------	|---------	|
| Create a Customer                	| ✔️        	| ✔️       	|
| Create Accounts for a Customer 	| ✔️        	| ✔️       	|
| Create a Token                   	| ✔️        	| ✔️       	|
| Connect an Account to a Tokens    | ✔️        	| ✔️       	|
| Check an Account's Token Balances | ✔️        	| ✔️       	|
| Fund an Account with Tokens      	| ✔️        	| ✔️       	|
| Pay other Accounts with Tokens   	| ✔️        	| ✔️       	|
| Payout a Token from an Account   	| ✔️        	| ✔️       	|
| Orderbook                        	| -        	| ✔     	|

---
## *Dependency Injection*
Inject the `ITokenServer` into your class:

```csharp
public class MyClass 
{
    public MyClass(ITokenServer tokenServer) 
    {
        ...
    }
}
```

Program.cs example:

```csharp
var services = new ServiceCollection();

services.AddTokenServer(o => ...);
services.AddScoped<MyClass>();

var provider = services.BuildServiceProvider();
var instance = provider.GetRequiredService<MyClass>();
```

Startup.cs example:

```csharp
public class Startup
{
    // Configure using TokenServerOptionsBuilder
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTokenServer(o => ...);
    }
}
```

```csharp
public class Startup
{
    // Configure using IConfiguration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddTokenServer(Configuration);
    }
}
```

---

## *Exceptions*

This SDK uses throws two types of custom exceptions: `NexusApiException` and `AuthProviderException`.

`NexusApiException` is thrown when Nexus API returns an error.

`AuthProviderException` is thrown when Nexus Identity returns an error.

Here is an example:

```csharp
try
{
    ...
}
catch (NexusApiException ex)
{
    // Handle api exception
    if (ex.StatusCode == 400 || ex.StatusCode == 404) 
    {
        // Handle user input error here
    } 
    else 
    {
        Console.WriteLine($"{ex.StatusCode}");
        Console.WriteLine($"{ex.Message}");
        Console.WriteLine($"{ex.ErrorCodes}");
    }  
}
catch (AuthProviderException ex)
{
    // Handle authorized exception
    Console.WriteLine($"{ex.Message}");
}
catch (Exception ex)
{
    // Handle unexpected exception
    Console.WriteLine($"{ex.Message}");
}
```
---

## *Logging*

The SDK supports the `ILogger` interface. Here is a basic example of how to add debug logging to the console using Serilog:

```csharp
using Serilog;

var logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.Console()
              .CreateLogger();

services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger, dispose: true));
```

---

## *Environments*
The SDK supports two environments for Nexus:

1. Nexus Test

Using options builder:
```csharp
services.AddTokenServer(o => o.ConnectToTest("clientId", "clientSecret"));
```

Using appsettings.json
```json
{
  "NexusOptions": {
    "ApiUrl": "https://testapi.quantoz.com",
    "PaymentMethodOptions": {
      "Funding": "your-test-funding-paymentMethod",
      "Payout": "your-test-funding-paymentMethod"
    },
    "AuthProviderOptions": {
      "IdentityUrl": "https://testidentity.quantoz.com",
      "ClientId": "your-test-clientId",
      "ClientSecret": "your-test-clientSecret"
    }
  }
}
```

2. Nexus Production 

Using options builder:
```csharp
services.AddTokenServer(o => o.ConnectToProduction("clientId", "clientSecret"));
```

Using appsettings.json
```json
{
  "NexusOptions": {
    "ApiUrl": "https://api.quantoz.com",
    "PaymentMethodOptions": {
      "Funding": "your-prod-funding-paymentMethod",
      "Payout": "your-prod-funding-paymentMethod"
    },
    "AuthProviderOptions": {
      "IdentityUrl": "https://identity.quantoz.com",
      "ClientId": "your-prod-clientId",
      "ClientSecret": "your-prod-clientSecret"
    }
  }
}
```

Check our [Nexus Docs](https://devdocs.quantoz.com/articles/start-developing/sd_authentication.html) for more information on how to generate a `clientId` and `clientSecret` for your environment.

---

## *Encryption & Decryption*
The private key of an Account should be treated like a password. The SDK supports AES encryption and decryption of an accounts private key to ensure that it is not visibly exposed in a database or on the users mobile phone.

To use AES encryption:

```csharp
services.UseSymmetricEncryption("KEY");
```

The length of the `KEY` can be 16 Bytes (~16 characters), 24 bytes (~24 characters) or 32 bytes (~32 characters) long. You can read up more on AES encryption [here:](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard).

Encrypt example:
```csharp
public class MyClass 
{
    public MyClass(ITokenServer tokenServer, IEncrypter encrypter) 
    {
         var kp = AlgorandKeyPair.Generate();
         var encryptedPrivateKey = kp.GetPrivateKey(_encrypter);
    }
}
```

Decrypt example:
```csharp
public class MyClass 
{
    public MyClass(ITokenServer tokenServer, IDecrypter encrypter) 
    {
        var encryptedPrivateKey = ""; // get from db or mobile phone storage
        var kp = AlgorandKeyPair.FromPrivateKey(encryptedPrivateKey, _decrypter);
    }
}
```

**IMPORTANT:** Make sure to safely store a backup of your AES `KEY`. If you lose it, you or your users will lose access to their Account.

---

## *Tokens*

### *Types*
Nexus supports two types of tokens: 

1: `StableCoin` - A token that is pegged to `currency` at a certain `rate`.

2: `AssetToken` - A token that is pegged to a physical asset with limited `supply`.

Stellar Settings:
- `account limit (none)` - The total amount of token a single Account can hold.
- `authorization required (true)` - This token can only be used in a closed system. This means that an account not known to Nexus cannot receive this token.
- `authorization revocable (true)` - Allow Nexus to revoke an accounts permission to hold it this token. This means that it can no longer send or receive more of this token.
- `clawback enabled (true)` - Allow Nexus to retrieve (clawback) this token from an account.

Algorand Settings:
- `total supply` - The total amount of token that can circulate in the system.
- `decimals` - The number of digits to use after the decimal point when displaying this asset. If set to 0, the asset is not divisible beyond its base unit. If set to 1, the base asset unit is tenths. If 2, the base asset unit is hundredths, and so on.
- `authorization revocable (true)` - Allow Nexus to revoke an accounts permission to hold it this token. This means that it can no longer send or receive more of this token.
- `clawback enabled (true)` - Allow Nexus to retrieve this token from an account.

**Note**: That once a token has been created these settings cannot be changed.


### *Taxonomy*

The idea of token taxonomy is to provide a token on a blockchain with more meaning than just a code. The first step is to specify a set of properties that your tokens must comply with, this is called a `schema` ([NJsonSchema](https://github.com/RicoSuter/NJsonSchema) is a library that can be used to easily generate a schema from an C# object). 

When creating a new token, you specify the `schema` along with a set of `properties` the token has. These `properties` are validated against the schema and hashed using the SHA256 algorithm. This `hash` is stored base64 encoded as a token property on the blockchain. It is also possible to add an `assetURL`, this url can be a reference to a webpage where the asset is described or can contain a JSON representation of the `properties`. 

Any person can verify that the `properties` of a created token have not been altered by encoding the JSON using SHA256 hashing algorithm and comparing it to the decoded `hash` on the blockchain. 

Alternatively, you can provide a hash to store on the blockchain, instead of having to provide a taxonomy schema and properties to calculate one, but note a provided hash is not validated in any way.  

---

## *Payment Methods*
Normally the `Funding` of a token and the `Payout` of a token involve an exchange of fiat currencies. You can find more information about Payment Methods [here.]("https://devdocs.quantoz.com/articles/configure-nexus/initial_setup.html?q=payment%20methods") To use a default Payment Method for all funding and payout requests you can configure the SDK to use a default ones instead:

Add default `funding` Payment Method:
```csharp
services.AddTokenServer(o => o.AddDefaultFundingPaymentMethod("FUNDING_PM"));
```

Add default `payout` Payment Method:
```csharp
services.AddTokenServer(o => o.AddDefaultPayoutPaymentMethod("PAYOUT_PM"));
```

If no default Payment Method is supplied, it will need to be specified on funding /payout level.

```csharp
tokenServer.Operations.CreateFundingAsync("accountCode", "tokenCode", 100, pm: "FUNDING_PM");

tokenServer.Operations.CreatePayoutAsync("accountCode", "tokenCode", 100, pm: "PAYOUT_PM");
```

---

## *Testing*

To allow for testability the `TokenServer` uses an `ITokenServerProvider` implementation. This means you can inject your own instance of this interface in your tests.

```csharp
public class MockTokenServerProvider : ITokenServerProvider
{
    public Task<SignableResponse> ConnectAccountToTokenAsync(string accountCode, string tokenCode)
    {
        throw new NotImplementedException();
    }

    public Task<SignableResponse> ConnectAccountToTokensAsync(string accountCode, string[] tokenCodes)
    {
        throw new NotImplementedException();
    }

    ...
}

var mock = new MockTokenServerProvider();
_services.AddTokenServer(mock);
```
---
## Examples
- [Stellar](../Nexus.Token.Stellar.Examples) examples of this SDK in action on the Stellar blockchain.
- [Algorand](../Nexus.Token.Algorand.Examples) examples of this SDK in action on the Algorand blockchain.