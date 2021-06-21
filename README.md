# SalesTax
This is a service that calculates sales tax for locations and by order using the TaxJar API (https://developers.taxjar.com/api/reference/#introduction)

Service consumption should be done through dependency injection using the `ISalesTaxService` interface.

## Usage

The service is designed for Dependency Injection, which can be configured in many ways. The SalesTaxService requires an injected IConfiguration and ILogger. In a class constructor, add something like the following:

```csharp
private readonly ISalesTaxService _service;
public void MyClass(ISalesTaxService service)
{
    _service = service ?? throw new ArgumentNullException(nameof(service));
}
```

To call the services:

```csharp
var order = BuildOrder(); // TODO
var result = await _service.CalculateSalesTaxByOrder(order);
```

```csharp
var result = await _service.CalculateSalesTaxByLocation("90404");
```

## Unit Tests
Contains integration tests to exercise the code. Order is mocked based on API documentation.

## Dependencies

* NUnit
* Newtonsoft.Json
* RestSharp

## Class Diagram

![UML Diagram](https://github.com/Usualdosage/SalesTax/blob/master/SalesTaxUML.png?raw=true)
