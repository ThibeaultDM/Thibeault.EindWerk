using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.EindWerk.Objects
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string City { get; set; } = string.Empty;
        public int PostCode { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;

        public override string ToString()
        {
            return "Address: " + City + ": " + PostCode.ToString() + "\n" + StreetName + " " + HouseNumber;
        }
    }
}