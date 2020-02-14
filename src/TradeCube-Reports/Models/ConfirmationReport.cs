using System.Collections.Generic;
using TradeCube_Reports.DataObjects;

namespace TradeCube_Reports.Models
{
    public class ConfirmationReport
    {
        public IEnumerable<TradeDataObject> Trades { get; set; }
    }
}
