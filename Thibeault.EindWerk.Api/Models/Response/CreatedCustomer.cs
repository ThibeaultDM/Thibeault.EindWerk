using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Api.Models.Response
{
    public class CreatedCustomer
    {
        public string TrackingNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        private List<BrokenRule> BrokenRules { get; set; }
    }
}