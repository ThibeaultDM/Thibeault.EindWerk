using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.Example.Objects.DataObjects
{
    public class Customer : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TrackingNumber { get; set; }
        public string FullName { get; set; } = string.Empty; // To prevent cannot save null exception on creation
        public string Email { get; set; } = string.Empty;
        public Address Address { get; set; } = new();
        public List<OrderHeader>? Orders { get; set; } = new(); // So I can do .Add immediately

        public override string ToString()
        {
            return "Customer: " + FullName + ": " + TrackingNumber.ToString();
        }
    }
}