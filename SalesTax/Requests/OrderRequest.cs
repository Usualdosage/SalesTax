using Newtonsoft.Json;
using System.Collections.Generic;

namespace SalesTax.Requests
{
    public class OrderRequest
    {
        [JsonProperty("from_country")]
        public string FromCountry { get; set; }

        [JsonProperty("from_zip")]
        public string FromZip { get; set; }

        [JsonProperty("from_state")]
        public string FromState { get; set; }

        [JsonProperty("from_city")]
        public string FromCity { get; set; }

        [JsonProperty("from_street")]
        public string FromStreet { get; set; }

        [JsonProperty("to_country")]
        public string ToCountry { get; set; }

        [JsonProperty("to_zip")]
        public string ToZip { get; set; }

        [JsonProperty("to_state")]
        public string ToState { get; set; }

        [JsonProperty("to_city")]
        public string ToCity { get; set; }

        [JsonProperty("to_street")]
        public string ToStreet { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("shipping")]
        public decimal Shipping { get; set; }

        [JsonProperty("nexus_addresses")]
        public List<NexusAddress> NexusAddresses { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
    }

    public sealed class NexusAddress
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }

    public sealed class LineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("prodicy_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty("unit_price")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
