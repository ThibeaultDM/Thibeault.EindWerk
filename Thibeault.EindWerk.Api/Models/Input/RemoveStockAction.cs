﻿namespace Thibeault.EindWerk.Api.Models.Input
{
    public class RemoveStockAction
    {
        public int ProductSerialNumber { get; set; }
        public int StockActionId { get; set; }
    }
}