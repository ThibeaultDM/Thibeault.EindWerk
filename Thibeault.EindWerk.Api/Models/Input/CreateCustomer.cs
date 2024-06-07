using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.Api.Models.Input
{
    public class CreateCustomer
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

    }
}
