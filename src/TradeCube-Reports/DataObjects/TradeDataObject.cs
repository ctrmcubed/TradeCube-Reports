﻿using System;

namespace TradeCube_Reports.DataObjects
{
    public class TradeDataObject
    {
        public string TradeReference { get; set; }
        public DateTime TradeDateTime { get; set; }
        public decimal QuantityUnit { get; set; }
        public ProductDataObject Product { get; set; }
        public PriceDataObject Price { get; set; }
        public QuantityDataObject Quantity { get; set; }
        public PartyDataObject Buyer { get; set; }
        public PartyDataObject Seller { get; set; }
    }
}
