using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.EindWerk.Objects.DataObjects
{
    public class Product : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SerialNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int Reserved { get; set; }
        public List<StockAction> StockActions { get; set; } = new();

        public override string ToString()
        {
            return "Product: " + SerialNumber.ToString() + " " + Name;
        }
    }
}