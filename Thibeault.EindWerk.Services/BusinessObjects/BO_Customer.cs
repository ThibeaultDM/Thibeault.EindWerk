using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.RulesFrameWork;

namespace Thibeault.EindWerk.Services.BusinessObjects
{
    public class BO_Customer : BusinessObject
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }


    }
}