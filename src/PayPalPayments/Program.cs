using System;
using System.Configuration;
using System.Linq;
using Nest;
using PayPal.Api;
using PayPalPayments.Helpers;

namespace PayPalPayments
{
    class Program
    {
        static void Main()
        {
            var payPalClient = Clients.GetPayPalApiContext();
            var elasticClient = Clients.GetElasticClient();
            var oldestDate = ConfigurationManager.AppSettings["OldestDate"];
            var pageSize = ConfigurationManager.AppSettings["PayPalPageSize"].ToInt();
            string nextId = null;

            do
            {
                var history = Payment.List(payPalClient, count: pageSize, startId: nextId, startTime: oldestDate);

                var payments = history
                    .payments
                    .Where(p => p.state == "approved")
                    .Select(Transformations.ToPaymentInfo)
                    .ToList();

                if (history.payments.Count == 1)
                    return;

                elasticClient.IndexMany(payments);

                var lastPayment = payments.Last();
                nextId = lastPayment.Id;

                Console.WriteLine($"Processed until {lastPayment.CreatedAt.ToShortDateString()}");
            }
            while (true);
        }
    }
}
