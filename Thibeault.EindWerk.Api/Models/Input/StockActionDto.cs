﻿namespace Thibeault.EindWerk.Api.Models.Input
{
    public class StockActionDto
    {
        public int Id { get; set; }
        public Objects.Enums.Action Action { get; set; }
        public int Amount { get; set; }
    }
}