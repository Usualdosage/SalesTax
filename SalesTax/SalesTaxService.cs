using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SalesTax.Contracts;
using SalesTax.Requests;
using SalesTax.Responses;
using System;
using System.Threading.Tasks;

namespace SalesTax
{
    public class SalesTaxService : ISalesTaxService
    {
        #region Private Members

        private readonly ILogger _logger;
        private IRestClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes the SalesTaxService.
        /// </summary>
        /// <remarks>
        /// Your code test is to simply create a Tax Service that can take a Tax Calculator in the class initialization and return the total tax that needs to be collected.
        /// We are going to use Tax Jar as one of our calculators.You will need to write a.Net client to talk to their API, do not use theirs.
        /// 
        /// Unsure why a TaxCalculator class (?) would need to be added to class constructor, so did not implement that bit.
        /// </remarks>
        /// </summary>
        /// <param name="configuration">Configuration to read from appsettings.json</param>
        /// <param name="logger">The logger</param>
        public SalesTaxService(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _apiKey = _configuration["SalesTaxAPIs:APIKey:Value"];
        }

        #endregion Constructor

        #region ISalesTaxService Implementation

        /// <summary>
        /// For a given zipcode, calculates the sales tax for that location.
        /// </summary>
        /// <param name="zipCode">The zipCode in XXXXX-XXXX or XXXXX format. US zipcodes only.</param>
        /// <returns>TaxRatesByLocationResponse</returns>
        public async Task<TaxRatesByLocationResponse> CalculateSalesTaxByLocation(string zipCode)
        {
            string endpoint = _configuration["SalesTaxAPIs:SalesTaxForLocation:Value"];
            
            if (!string.IsNullOrWhiteSpace(endpoint))
            {
                try
                {
                    _client = new RestClient($"{endpoint}/{zipCode}");

                    var restRequest = new RestRequest();
                    restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _apiKey));
                    restRequest.AddHeader("Content-Type", "application/json");

                    var resp = await _client.ExecuteGetAsync(restRequest);

                    var response = JsonConvert.DeserializeObject<TaxRatesByLocationResponse>(resp.Content);

                    response.Success = true;

                    return response;
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc.Message, exc);

                    return new TaxRatesByLocationResponse() {
                        Success = false,
                        ErrorMessage = exc.ToString()
                    };
                }
            }
            else
            {
                throw new ArgumentNullException(endpoint);
            }
            
        }

        /// <summary>
        /// Calculates the sales tax for an order with multiple addresses and/or line items.
        /// </summary>
        /// <param name="request">OrderRequest</param>
        /// <returns>SalesTaxByOrderResponse</returns>
        public async Task<SalesTaxByOrderResponse> CalculateSalesTaxByOrder(OrderRequest request)
        {
            string endpoint = _configuration["SalesTaxAPIs:SalesTaxForOrder:Value"];

            if (!string.IsNullOrWhiteSpace(endpoint))
            {
                try
                {
                    _client = new RestClient($"{endpoint}");

                    var restRequest = new RestRequest(endpoint);
                    restRequest.AddHeader("Authorization", string.Format("Bearer {0}", _apiKey));
                    restRequest.AddHeader("Content-Type", "application/json");
                    restRequest.AddJsonBody(JsonConvert.SerializeObject(request));

                    var resp = await _client.ExecutePostAsync(restRequest);

                    var response = JsonConvert.DeserializeObject<SalesTaxByOrderResponse>(resp.Content);

                    return response;
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc.Message, exc);

                    return new SalesTaxByOrderResponse()
                    {
                        Success = false,
                        ErrorMessage = exc.ToString()
                    };
                }
            }
            else
            {
                throw new ArgumentNullException(endpoint);
            }
        }

        #endregion ISalesTaxService Implementation
    }
}
