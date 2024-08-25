using Thibeault.Example.Objects.Enums;

namespace Thibeault.Example.Api.Models.Response
{
    public class OrderHeaderResponse
    {
        public int TrackingNumber { get; set; }
        public CustomerResponse Customer { get; set; }
        public Status Status { get; set; } = Status.New;
        public List<StockActionResponseForOrderHeader> StockActions { get; set; } = new();
    }
}