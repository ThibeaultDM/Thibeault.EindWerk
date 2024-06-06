namespace Thibeault.EindWerk.Base
{
    public class BaseObject
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}