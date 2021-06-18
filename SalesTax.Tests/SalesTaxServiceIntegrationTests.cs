using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SalesTax.Contracts;
using SalesTax.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SalesTax.Tests
{
    [TestFixture]
    public class SalesTaxServiceIntegrationTests
    {
        #region Private Members

        private ISalesTaxService _service;

        #endregion Private Members

        #region Setup

        [OneTimeSetUp]
        public void Setup()
        {
            // Initialize the configuration system so we can read information from the appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            // Create a logger factory from which to build the logger for all of the tests
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "hh:mm:ss ";
                }));

            ILogger<SalesTaxServiceIntegrationTests> logger = loggerFactory.CreateLogger<SalesTaxServiceIntegrationTests>();

            _service = new SalesTaxService(config, logger);
        }

        #endregion Setup

        #region Test Methods

        [TestCase(ExpectedResult = TestResult.Completes)]
        public async Task<TestResult> CalculateSalesTaxByOrder_IntegrationTest()
        {
            try
            {
                // Arrange
                var order = BuildOrderRequest();

                // Act
                var result = await _service.CalculateSalesTaxByOrder(order);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.TaxRate);
                Assert.IsTrue(result.Success);

                return TestResult.Completes;
            }
            catch
            {
                return TestResult.ThrowsException;
            }
        }

        [TestCase("90404", ExpectedResult = TestResult.Completes)]
        [TestCase("05495-2086", ExpectedResult = TestResult.Completes)]
        [TestCase("90404-3370", ExpectedResult = TestResult.Completes)]
        [TestCase(null, ExpectedResult = TestResult.ReturnsNull)]
        public async Task<TestResult> CalculateSalesTaxByLocation_IntegrationTest(string zipCode)
        {
            try
            {
                // Act
                var result = await _service.CalculateSalesTaxByLocation(zipCode);

                if (zipCode == null)
                    return TestResult.ReturnsNull;

                // Assert
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.TaxRate);
                Assert.IsTrue(result.Success);

                return TestResult.Completes;
            }
            catch
            {
                return TestResult.ThrowsException;
            }
        }


        #endregion Test Methods

        #region Private Methods

        private OrderRequest BuildOrderRequest()
        {
            var request = new OrderRequest()
            {
                FromCountry = "US",
                FromZip = "92093",
                FromState = "CA",
                FromCity = "La Jolla",
                FromStreet = "9500 Gilman Drive",
                ToCountry = "US",
                ToZip = "92002",
                ToState = "CA",
                ToCity = "Los Angeles",
                ToStreet = "1335 E 103rd St",
                Amount = 15m,
                Shipping = 1.5m,
                NexusAddresses = new List<NexusAddress>() {
                    new NexusAddress() {
                        Id = "Main Location",
                        Country = "US",
                        Zip = "92023",
                        State = "CA",
                        City = "La Jolla",
                        Street = "9500 Gilman Drive"
                    }
                },
                LineItems = new List<LineItem>() {
                    new LineItem() {
                        Id="1",
                        Quantity = 1m,
                        ProductTaxCode = "20010",
                        UnitPrice = 15m,
                        Discount = 0m
                    }
                }
            };

            return request;
        }

        #endregion Private Methods
    }
}
