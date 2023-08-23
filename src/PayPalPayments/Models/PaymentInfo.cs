using System;
using System.Collections.Generic;

namespace PayPalPayments.Models
{
    public class PaymentInfo
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public List<string> CartItems { get; set; }
        public bool WasRefunded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
