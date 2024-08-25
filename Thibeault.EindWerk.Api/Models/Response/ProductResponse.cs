using Thibeault.Example.Api.Models.Input;

namespace Thibeault.Example.Api.Models.Response
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