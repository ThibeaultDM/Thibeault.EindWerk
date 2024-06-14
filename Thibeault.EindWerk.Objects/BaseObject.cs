using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.Objects

{
    public class BaseObject
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Product Product { get; }
        public Enums.Action Action { get; }
        public int Amount { get; }
    }
}