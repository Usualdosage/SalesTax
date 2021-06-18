using SalesTax.Requests;
using System.Collections.Generic;

namespace SalesTax.Tests
{
    public class TestData
    {
        /// <summary>
        /// Creates a mock order request for testing.
        /// </summary>
        /// <returns></returns>
        public static OrderRequest BuildOrderRequest()
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
    }
}
