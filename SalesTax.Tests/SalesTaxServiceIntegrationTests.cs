using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SalesTax.Contracts;
using System;
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
                var order = TestData.BuildOrderRequest();

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
    }
}
