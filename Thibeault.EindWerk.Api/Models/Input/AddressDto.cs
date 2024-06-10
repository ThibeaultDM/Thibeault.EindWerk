namespace Thibeault.EindWerk.Api.Models.Input
{
    public class AddressDto
    {
        public string City { get; set; }
        public int PostCode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
    }
}