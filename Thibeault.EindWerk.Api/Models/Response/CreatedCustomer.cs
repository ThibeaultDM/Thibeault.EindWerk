using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.Api.Models.Response
{
    public class CreatedCustomer
    {
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
