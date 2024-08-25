namespace Thibeault.Example.Api.Models.Input
{
    public class AddressDto
    {
        public string City { get; set; }
        public int PostCode { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }

        public override string ToString()
        {
            return "Addresses: " + City + ": " + PostCode.ToString() + "\n" + StreetName + " " + HouseNumber;
        }
    }
}