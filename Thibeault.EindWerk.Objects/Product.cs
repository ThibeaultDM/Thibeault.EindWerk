using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Thibeault.EindWerk.Objects
{
    public class Product : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
    }
}