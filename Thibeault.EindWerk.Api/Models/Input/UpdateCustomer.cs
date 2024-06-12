namespace Thibeault.EindWerk.Api.Models.Input
{
    public class UpdateCustomer
    {
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}