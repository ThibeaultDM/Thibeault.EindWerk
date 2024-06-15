using Thibeault.EindWerk.Api.Models.Response;

namespace Thibeault.EindWerk.Api.Models.Input
{
    public class AddOrderHeader
    {
        public string CustomerTrackingNumber { get; set; }

        public List<StockActionResponseForOrderHeader> StockActions { get; set; }
    }
}