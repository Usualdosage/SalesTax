using System;

namespace SalesTax.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime? ResponseDt { get => DateTime.UtcNow; }
    }
}
