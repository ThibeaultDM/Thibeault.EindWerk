using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Thibeault.EindWerk.Objects
{
    public class Customer : BaseObject
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}