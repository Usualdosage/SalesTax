
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestSharp;
using SalesTax.Contracts;
using System;

namespace SalesTax.Tests
{
    [TestFixture]
    public class SalesTaxServiceMockTests
    {
        private ISalesTaxService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            var mockConfig = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger>();
            var mockClient = new Mock<IRestClient>(); // Add methods

            _service = new SalesTaxService(mockConfig.Object, mockLogger.Object);
        }

        [TestCase]
        public void CalculateSalesTaxByLocation_MockTest()
        {
            try
            {
              
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestCase]
        public void CalculateSalesTaxByOrder_MockTest()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
