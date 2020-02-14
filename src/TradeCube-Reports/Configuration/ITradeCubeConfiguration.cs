namespace TradeCube_Reports.Configuration
{
    public interface ITradeCubeConfiguration
    {
        string TradeCubeApiKey { get; }
        string TradeCubeApiBaseAddress { get; }
    }
}