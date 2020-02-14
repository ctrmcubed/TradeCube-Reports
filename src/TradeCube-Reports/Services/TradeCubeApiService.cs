using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TradeCube_Reports.Configuration;

namespace TradeCube_Reports.Services
{
    public class TradeCubeApiService : TradeCubeApiServiceBase
    {
        private readonly ILogger<TradeCubeApiService> logger;

        public TradeCubeApiService(IHttpClientFactory httpClientFactory, ITradeCubeConfiguration tradeCubeConfiguration,
            ILogger<TradeCubeApiService> logger) : base(httpClientFactory, tradeCubeConfiguration)
        {
            this.logger = logger;
        }

        public async Task<TV> Post<TV>(string apiKey, JObject request)
        {
            try
            {
                var client = CreateClient(apiKey);
                var response = await client.PostAsJsonAsync("Trade/query", request);

                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync();

                logger.LogDebug(responseStream.ToString());

                var data = await JsonSerializer.DeserializeAsync<TV>(responseStream);

                return data;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
