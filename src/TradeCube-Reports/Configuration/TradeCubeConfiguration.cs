using System;

namespace TradeCube_Reports.Configuration
{
    public class TradeCubeConfiguration : ITradeCubeConfiguration
    {
        public string TradeCubeApiKey { get; }
        public string TradeCubeApiBaseAddress { get; }

        public TradeCubeConfiguration()
        {
            TradeCubeApiKey = Environment.GetEnvironmentVariable("TRADECUBE_APIKEY");
            TradeCubeApiBaseAddress = Environment.GetEnvironmentVariable("TRADECUBE_APIBASEADDRESS");
        }
    }
}
