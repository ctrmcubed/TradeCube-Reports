using System.Collections.Generic;
using System.Threading.Tasks;
using TradeCube_Reports.DataObjects;
using TradeCube_Reports.Messages;

namespace TradeCube_Reports.Services
{
    public interface ITradeService
    {
        Task<ApiResponseWrapper<IEnumerable<TradeDataObject>>> Trades(string apiKey, TradeRequest tradeReferences);
    }
}