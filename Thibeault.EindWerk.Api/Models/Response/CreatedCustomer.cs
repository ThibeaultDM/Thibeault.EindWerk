using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Api.Models.Response
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