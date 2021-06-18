using Newtonsoft.Json;

namespace SalesTax.Responses
{
    [JsonObject("rate")]
    public class TaxRatesByLocationResponse : BaseResponse
    {
        [JsonProperty("rate")]
        public Rate TaxRate { get; set; }

        public sealed class Rate
        {
            [JsonProperty("zip")]
            public string Zip { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("state_rate")]
            public decimal StateRate { get; set; }

            [JsonProperty("county")]
            public string County { get; set; }

            [JsonProperty("county_rate")]
            public decimal CountyRate { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("city_rate")]
            public decimal CityRate { get; set; }

            [JsonProperty("combined_district_rate")]
            public decimal CombinedDistrictRate { get; set; }

            [JsonProperty("combined_rate")]
            public decimal CombinedRate { get; set; }

            [JsonProperty("freight_taxable")]
            public bool FreightTaxable { get; set; }
        }
    }
}
