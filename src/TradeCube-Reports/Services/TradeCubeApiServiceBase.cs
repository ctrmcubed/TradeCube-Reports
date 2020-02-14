using System;
using System.Net.Http;
using TradeCube_Reports.Configuration;

namespace TradeCube_Reports.Services
{
    public class TradeCubeApiServiceBase
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ITradeCubeConfiguration tradeCubeConfiguration;

        protected TradeCubeApiServiceBase(IHttpClientFactory clientFactory, ITradeCubeConfiguration tradeCubeConfiguration)
        {
            this.clientFactory = clientFactory;
            this.tradeCubeConfiguration = tradeCubeConfiguration;
        }

        protected HttpClient CreateClient(string apiKey)
        {
            var client = clientFactory.CreateClient();

            client.BaseAddress = new Uri(tradeCubeConfiguration.TradeCubeApiBaseAddress);
            client.DefaultRequestHeaders.Add("apiKey", apiKey);

            return client;
        }
    }
}