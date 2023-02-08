## [2023.2.8.9](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/12370921)

### Breaking Changes
* Changed `BankAccountBuilder` structure. See the examples for more information.
* Changed `CustomerBuilder` to `CreateCustomerBuilder`. See the examples for more information.

### New Features
* Added `UpdateCustomerBuilder` for updating an existing customer.
* Added support for parallelization.
* Added support for simulating a payout to estimate fees. See the `Operations.SimulatePayoutAsync` for more information.

### Bug Fixes
* Fixed a bug where the `RequestedAmount` property is `null` for `PayoutResponse`.
* Fixed a bug where funding an account returned `CustomerNotFoundError`.

## [2023.1.3](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11619445)

### New Features
* Updated to dotnet sdk to version 7.0

### Bug Fixes
* Fixed a bug where executing any request in parallel would could sometimes throw a 'URL segments have already been added.' exception.

### Breaking Changes
* The namespaces of AuthProviderException and NexusApiException has been updated to: Nexus.SDK.Shared.ErrorHandling
* The default section name for injecting the token server via the configuration has been renamed from 'TokenServerOptions' to 'NexusOptions' 

## [2023.1.3](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11619445)

### New Features
* Added isBusiness property to customer creation and fetching

## [2022.12.30.11](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11596598)

### New Features
* Added functionality to get and filter on accounts

## [2022.12.23.10](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11530311)

### Bug Fixes
* Fixed a json parsing bug on the trust level property of a customer response.

## [2022.12.21.13](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11489069)

### Other Changes
* Updated token production urls for ConnectToProduction(clientId, clientSecret)

## [2022.12.13.11](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11322442)

### New Features
* Added functionality to retrieve information about the partner's trust levels and their buy and sell limits.

## [2022.12.1.9](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11104395)

### New Features
* Added new account response model for TokenOperationResponse.

## [2022.11.29.13](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11055409)

### Bug Fixes
* Fixed a json parsing bug on the public key property of an account.

### New Features
* Extended token operation details to also return the token code.

## [2022.11.28.8](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/11015559)

### New Features
* Updated customer response to include bank account number and optional properties. Check out the create account functionality in the examples projects for more information.

## [2022.11.25.11](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/10994394)

### New Features
* Added functionality to retrieve information about a customer's token funding and payout limits using a customer code and tokenCode. Check out the limits functionality in the Stellar and Algorand example projects for more information.

### Bug Fixes
* Update customer creation to also accept a bank account number.

## [2022.11.15.9](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/10632663)

### New Features
* Added functionality to list token operation details according to code and query parameters.

## [2022.11.15.12](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/10637406)

### New Features
* Added functionality to retrieve customer personal information using a customer code.

## [2022.10.11.21](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/9847275)

### New Features
* It is now possible to provide your own hash during token creation. Check out the examples and documentation for more information. 
* The token details now returns its blockchain ID. This is the unique identifier of the token on the blockchain. For Algorand this is represented as an unsigned long: 312769. For Stellar its a combination of asset issuer and code: {issuerAddress}:{assetCode}.

## [2022.9.25.16](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/9474509)

### New Features
* Added functionality for TokenServer to be configured using IConfiguration. Check out the documentation for more
information.

## [2022.8.30.11](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/8923295)

### Bug Fixes
* Fixed a json parsing bug when initiating the cancelling a token order.

### Breaking Changes
* Order cancellation now returns a `SignableResponse` instead of a `CancelledOrderResponse`.

## [2022.8.26.12](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/8862706)

### Breaking Changes
* Updated customer creation to use `CustomerRequest`. Check out the create account functionality in the examples projects for more information.

### New Features
* Added `CustomerRequestBuilder` to help easy the construction of a `CustomerRequest`. Check out the create account functionality in the examples projects for more information.

### Other Changes
* Added checks to the `OrderRequestBuilder`.

### Bug Fixes
* Updated taxonomy creation to use the correct json properties.

## [2022.10.5.7](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/9699238)

### New Features
* Extended customer creation `CustomerRequest` to support optional properties. Check out the create account functionality in the examples projects for more information.
