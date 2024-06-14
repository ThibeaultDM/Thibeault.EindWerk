namespace Thibeault.EindWerk.Api.Models.Response
{
    public class ProductResponseForStockAction
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
    }
}