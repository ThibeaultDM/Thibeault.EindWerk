namespace Thibeault.EindWerk.Api.Models.Input
{
    public class ChangeCustomerOrderHeader
    {
        public int OrderHeaderTrackingNumber { get; set; }
        public string CustomerTrackingNumber { get; set; }
    }
}