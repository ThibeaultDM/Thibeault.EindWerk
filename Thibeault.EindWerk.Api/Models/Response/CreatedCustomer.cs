using Thibeault.Example.Api.Models.Input;
using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Api.Models.Response
{
    public class CreatedCustomer
    {
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }

        public List<BrokenRule> BrokenRules { get; set; }
    }
}