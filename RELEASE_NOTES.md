## [2022.8.30.11](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/8923295)

### Bug Fixes
* Fixed a json parsing bug when initiating the cancelling a token order.

### Breaking Changes
* Order cancellationg now returns a `SignableResponse` instead of a `CancelledOrderResponse`.

## [2022.8.26.12](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/8862706)

### Breaking Changes
* Updated customer creation to use `CustomerRequest`. Check out the create account functionality in the examples projects for more information.

### New Features
* Added `CustomerRequestBuilder` to help easy the construction of a `CustomerRequest`. Check out the create account functionality in the examples projects for more information.

### Other Changes
* Added checks to the `OrderRequestBuilder`.
