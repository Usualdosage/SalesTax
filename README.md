# SalesTax
This is a service that calculates sales tax for locations and by order using the TaxJar API (https://developers.taxjar.com/api/reference/#introduction)

Service consumption should be done through dependency injection using the `ISalesTaxService` interface.

## Unit Tests
Contains integration tests to exercise the code. Order is mocked based on API documentation.

## Dependencies

* NUnit
* Newtonsoft.Json
* RestSharp

## Class Diagram

![UML Diagram](https://github.com/Usualdosage/SalesTax/blob/master/SalesTaxUML.png?raw=true)
