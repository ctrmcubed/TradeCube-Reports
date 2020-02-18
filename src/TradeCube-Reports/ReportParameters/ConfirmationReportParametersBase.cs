using System.Collections.Generic;

namespace TradeCube_Reports.ReportParameters
{
    public class ConfirmationReportParametersBase : ReportParametersBase
    {
        public string Template { get; set; }
        public IEnumerable<string> TradeReferences { get; set; }
    }
}
