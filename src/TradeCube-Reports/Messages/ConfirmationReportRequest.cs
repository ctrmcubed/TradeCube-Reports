﻿using System.Collections.Generic;

namespace TradeCube_Reports.Messages
{
    public class ConfirmationReportRequest
    {
        public string Template { get; set; }
        public IEnumerable<string> TradeReferences { get; set; }
    }
}