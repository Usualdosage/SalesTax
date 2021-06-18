using System;

namespace SalesTax.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public static DateTime? ResponseDt { get => DateTime.UtcNow; }
    }
}
