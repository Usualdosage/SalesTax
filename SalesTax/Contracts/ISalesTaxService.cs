using SalesTax.Requests;
using SalesTax.Responses;
using System.Threading.Tasks;

namespace SalesTax.Contracts
{
    public interface ISalesTaxService
    {
        Task<TaxRatesByLocationResponse> CalculateSalesTaxByLocation(string zipCode);

        Task<SalesTaxByOrderResponse> CalculateSalesTaxByOrder(OrderRequest request);
    }
}
