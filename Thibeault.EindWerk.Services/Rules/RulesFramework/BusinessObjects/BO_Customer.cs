using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.Services.Rules.RulesFramework.BusinessObjects
{
    public class BO_Customer : BusinessObject
    {
        private string trackingNumber;

        // nullable so that the map asigns the value correctly
        public int? Id { get; set; } = null;

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
            Rules.Add(new CustomerRule().IsRequired(nameof(FullName), FullName));
            Rules.Add(new CustomerRule().IsRequired(nameof(Email), Email));
            Rules.Add(new CustomerRule().IsRequired(nameof(Address), Address));

            Rules.Add(new CustomerRule().GenerateTrackingNumber(nameof(TrackingNumber), (int)Id, out trackingNumber));
            Rules.Add(new CustomerRule().ValiditieTrackingNumber(nameof(TrackingNumber), (int)Id, TrackingNumber));
            
            return base.AddBusinessRules();
        }
    }
}