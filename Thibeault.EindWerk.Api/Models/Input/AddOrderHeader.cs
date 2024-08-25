using Thibeault.Example.Api.Models.Response;

namespace Thibeault.Example.Api.Models.Input
{
    public class AddOrderHeader
    {
        public string CustomerTrackingNumber { get; set; }

        public List<StockActionResponseForOrderHeader> StockActions { get; set; }
    }
}