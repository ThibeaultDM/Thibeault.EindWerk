namespace Thibeault.EindWerk.Api.Models.Input
{
    public class CreateCustomer
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}