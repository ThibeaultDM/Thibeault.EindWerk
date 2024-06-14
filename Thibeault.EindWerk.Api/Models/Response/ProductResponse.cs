using Thibeault.EindWerk.Api.Models.Input;

namespace Thibeault.EindWerk.Api.Models.Response
{
    public class ProductResponse
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
        public List<StockActionDto> StockActions { get; set; }
    }
}
