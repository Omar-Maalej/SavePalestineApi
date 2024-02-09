using System.ComponentModel.DataAnnotations;

namespace SavePalestineApi.Models
{
    public class PaymentIntentCreateRequest
    {
        [Range(1, long.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public long Amount { get; set; }
        public string Currency { get; set; } = "usd"; 
    }
}
