using Newtonsoft.Json;
using SalesTax.Requests;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SalesTax.Responses
{
    public class SalesTaxByOrderResponse : BaseResponse
    {
        [JsonProperty("tax")]
        public Tax TaxRate { get; set; }

        public sealed class Tax
        {
            [JsonProperty("order_total_amount")]
            public decimal OrderTotalAmount { get; set; }

            [JsonProperty("shipping")]
            public decimal Shipping { get; set; }

            [JsonProperty("taxable_amount")]
            public decimal TaxableAmount { get; set; }

            [JsonProperty("amount_to_collect")]
            public decimal AmountToCollect { get; set; }

            [JsonProperty("rate")]
            public decimal Rate { get; set; }

            [JsonProperty("has_nexus")]
            public bool HasNexus { get; set; }

            [JsonProperty("freight_taxable")]
            public bool FreightTaxable { get; set; }

            [JsonProperty("tax_source")]
            public string TaxSource { get; set; }

            [JsonProperty("jurisdictions")]
            public Jurisdictions Jurisdictions { get; set; }

            [JsonProperty("breakdown")]
            public Breakdown Breakdown { get; set; }
        }

        public sealed class Jurisdictions
        {
            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }
        }

        public sealed class Breakdown
        {
            [JsonProperty("taxable_amount")]
            public decimal TaxableAmount { get; set; }

            [JsonProperty("tax_collectable")]
            public decimal TaxCollectable { get; set; }

            [JsonProperty("combined_tax_rate")]
            public decimal CombinedTaxRate { get; set; }

            [JsonProperty("state_taxable_amount")]
            public decimal StateTaxableAmount { get; set; }

            [JsonProperty("state_tax_rate")]
            public decimal StateTaxRate { get; set; }

            [JsonProperty("state_tax_collectable")]
            public decimal StateTaxCollectable { get; set; }

            [JsonProperty("county_taxable_amount")]
            public decimal CountyTaxableAmount { get; set; }

            [JsonProperty("county_tax_rate")]
            public decimal CountyTaxRate { get; set; }

            [JsonProperty("county_tax_collectable")]
            public decimal CountyTaxCollectable { get; set; }

            [JsonProperty("city_taxable_amount")]
            public decimal CityTaxableAmount { get; set; }

            [ExcludeFromCodeCoverage]
            [JsonProperty("city_tax_rate")]
            public decimal CityTaxRate { get; set; }

            [ExcludeFromCodeCoverage]
            [JsonProperty("city_tax_collectable")]
            public decimal CityTaxCollectable { get; set; }

            [JsonProperty("special_district_taxable_amount")]
            public decimal SpecialDistrictTaxableAmount { get; set; }

            [JsonProperty("special_district_tax_collectable")]
            public decimal SpecialDistrictTaxCollectable { get; set; }

            [JsonProperty("line_items")]
            public List<LineItem> LineItems { get; set; }
        }
    }
}
