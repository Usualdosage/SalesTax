using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SalesTax.Contracts;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace SalesTax.Tests
{
    [ExcludeFromCodeCoverage]
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
            _service = new SalesTaxService(GetConfig(), GetLogger());
        }

        #endregion Setup

        #region Test Methods

        [TestCase(true, true, ExpectedResult = TestResult.Completes)]
        [TestCase(false, true, ExpectedResult = TestResult.ThrowsException)]
        [TestCase(false, false, ExpectedResult = TestResult.ThrowsException)]
        [TestCase(true, false, ExpectedResult = TestResult.ThrowsException)]
        public async Task<TestResult> CalculateSalesTaxByOrder_IntegrationTest(bool validConfig, bool validLogger)
        {
            try
            {
                // Arrange
                var order = TestData.BuildOrderRequest();
                _service = new SalesTaxService(validConfig ? GetConfig() : GetConfig("appsettings.test.json"), validLogger ? GetLogger() : null);

                // Act
                var result = await _service.CalculateSalesTaxByOrder(order);

                // Assert
                if (!validConfig)
                {
                    Assert.IsNotNull(result);
                    Assert.IsFalse(result.Success);
                    return TestResult.ThrowsException;
                }
                else
                {
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result?.TaxRate);
                    Assert.IsNotNull(result?.TaxRate?.Jurisdictions);
                    Assert.IsNotNull(result?.TaxRate?.Breakdown);
                    Assert.IsTrue(result.Success);
                    return TestResult.Completes;
                }
            }
            catch
            {
                return TestResult.ThrowsException;
            }
        }

        [TestCase(false, true, "90404", ExpectedResult = TestResult.ThrowsException)]
        [TestCase(false, false, "90404", ExpectedResult = TestResult.ThrowsException)]
        [TestCase(true, false, "90404", ExpectedResult = TestResult.ThrowsException)]
        [TestCase(true, true, "90404", ExpectedResult = TestResult.Completes)]
        [TestCase(true, true, "05495-2086", ExpectedResult = TestResult.Completes)]
        [TestCase(true, true, "90404-3370", ExpectedResult = TestResult.Completes)]
        [TestCase(true, true, null, ExpectedResult = TestResult.ReturnsNull)]
        [TestCase(true, true, "", ExpectedResult = TestResult.ReturnsNull)]
        [TestCase(true, true, "INVALID", ExpectedResult = TestResult.ReturnsNull)]
        public async Task<TestResult> CalculateSalesTaxByLocation_IntegrationTest(bool validConfig, bool validLogger, string zipCode)
        {
            try
            {
                // Arrange
                _service = new SalesTaxService(validConfig ? GetConfig() : GetConfig("appsettings.test.json"), validLogger ? GetLogger() : null);

                // Act
                var result = await _service.CalculateSalesTaxByLocation(zipCode);

                if (string.IsNullOrWhiteSpace(zipCode))
                {
                    return TestResult.ReturnsNull;
                }

                // Assert
                if (zipCode == "INVALID")
                {
                    Assert.IsNull(result?.TaxRate);
                    return TestResult.ReturnsNull;
                }                
                else if (!validConfig)
                {
                    Assert.IsNotNull(result);
                    Assert.IsFalse(result.Success);
                    return TestResult.ThrowsException;
                }
                else
                {
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result?.TaxRate);
                    Assert.IsTrue(result.Success);
                    Assert.IsNull(result.ErrorMessage);
                    Assert.IsTrue(result.ResponseDt.HasValue);
                    return TestResult.Completes;
                }
            }
            catch
            {
                return TestResult.ThrowsException;
            }
        }


        #endregion Test Methods

        #region Private Methods

        private IConfiguration GetConfig(string configFileName = "appsettings.json")
        {
            // Initialize the configuration system so we can read information from the appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile(configFileName, optional: false);

            return builder.Build();
        }

        private ILogger GetLogger()
        {
            // Create a logger factory from which to build the logger for all of the tests
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "hh:mm:ss ";
                }));

            return loggerFactory.CreateLogger<SalesTaxServiceIntegrationTests>();
        }

        #endregion Private Methods
    }
}
