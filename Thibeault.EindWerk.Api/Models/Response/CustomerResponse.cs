using Thibeault.EindWerk.Api.Models.Input;

namespace Thibeault.EindWerk.Api.Models.Response
{
    public class CustomerResponse
    {
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}