using System.Collections.Generic;

namespace TradeCube_Reports.Messages
{
    public class TradeRequest
    {
        public IEnumerable<string> TradeReferences { get; set; }
    }
}
