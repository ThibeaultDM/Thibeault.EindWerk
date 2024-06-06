namespace Thibeault.EindWerk.Base
{
    public class Customer : BaseObject
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}