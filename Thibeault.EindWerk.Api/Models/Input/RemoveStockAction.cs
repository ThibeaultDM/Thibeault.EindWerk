namespace Thibeault.EindWerk.Api.Models.Input
{
    public class RemoveStockAction
    {
        public int ProductSerialNumber { get; set; }
        public Guid StockActionId { get; set; }
    }
}