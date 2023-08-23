using System;
using System.Configuration;
using Nest;
using PayPal.Api;
using PayPalPayments.Models;

namespace PayPalPayments.Helpers
{
    public static class Clients
    {
        public static APIContext GetPayPalApiContext()
        {
            var config = ConfigManager.Instance.GetProperties();
            var oAuth = new OAuthTokenCredential(config["clientId"], config["clientSecret"], config);
            return new APIContext(oAuth.GetAccessToken());
        }

        public static ElasticClient GetElasticClient()
        {
            var endpoint = ConfigurationManager.AppSettings["ElasticsearchEndpoint"];
            var indexName = ConfigurationManager.AppSettings["ElasticsearchIndexName"];

            var uri = new Uri(endpoint);
            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(indexName);
            settings.MapDefaultTypeNames(config => config.Add(typeof(PaymentInfo), indexName));

            return new ElasticClient(settings);
        }
    }
}
