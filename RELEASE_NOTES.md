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


## [2022.10.5.7](https://gitlab.com/quantoz-public/nexus-sdk-dotnet/-/packages/9699238)

### New Features
* Extended customer creation `CustomerRequest` to support optional properties. Check out the create account functionality in the examples projects for more information.
