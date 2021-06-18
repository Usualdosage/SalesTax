# SalesTax
This is a service that calculates sales tax for locations and by order using the TaxJar API (https://developers.taxjar.com/api/reference/#introduction)

Service consumption should be done through dependency injection using the `ISalesTaxService` interface.

## Unit Tests
Contains both mock and integration tests to exercise the code. Mock tests do not call the actual API, whereas the integration tests do.

## Dependencies

* NUnit
* Newtonsoft.Json
* RestSharp
