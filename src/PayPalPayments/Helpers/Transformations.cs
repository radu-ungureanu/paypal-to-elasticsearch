using System;
using System.Linq;
using PayPal.Api;
using PayPalPayments.Models;

namespace PayPalPayments.Helpers
{
    public static class Transformations
    {
        public static PaymentInfo ToPaymentInfo(Payment payment)
        {
            var transaction = payment.transactions.First();

            return new PaymentInfo
            {
                Id = payment.id,
                Amount = transaction.amount.total.ToDouble(),
                WasRefunded = transaction.related_resources.Any(r => r.refund != null),
                CartItems = transaction.item_list.items.Select(item => item.name).ToList(),
                CreatedAt = payment.create_time.ToDateTime(),
                UpdatedAt = payment.update_time?.ToDateTime()
            };
        }
    }
}
