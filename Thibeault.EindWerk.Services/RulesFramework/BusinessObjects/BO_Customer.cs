using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.RulesFramework;
using Thibeault.EindWerk.Services.RulesFrameWork;

namespace Thibeault.EindWerk.Services.RulesFramework.BusinessObjects
{
    public class BO_Customer : BusinessObject
    {
        private string trackingNumber;

        public int Id { get; set; } = 0;

        public string TrackingNumber
        {
            get { return trackingNumber; }
            set { trackingNumber = value; }
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public override bool AddBusinessRules()
        {
            Rules.Add(new CustomerRule().IsRequired(nameof(Id), Id));
            Rules.Add(new CustomerRule().IsRequired(nameof(TrackingNumber), TrackingNumber));
            Rules.Add(new CustomerRule().IsRequired(nameof(FullName), FullName));
            Rules.Add(new CustomerRule().IsRequired(nameof(Email), Email));
            Rules.Add(new CustomerRule().IsRequired(nameof(Address), Address));

            Rules.Add(new CustomerRule().GenerateTrackingNumber(nameof(TrackingNumber), Id, out trackingNumber));
            Rules.Add(new CustomerRule().ValiditieTrackingNumber(nameof(TrackingNumber), TrackingNumber));
            return base.AddBusinessRules();
        }
    }
}