﻿namespace Thibeault.EindWerk.Api.Models.Input
{
    public class AddStockAction
    {
        public int ProductSerialNumber { get; set; }
        public StockActionDto StockAction { get; set; }
    }
}