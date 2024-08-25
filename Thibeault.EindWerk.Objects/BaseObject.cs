namespace Thibeault.Example.Objects

{
    public class BaseObject
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}