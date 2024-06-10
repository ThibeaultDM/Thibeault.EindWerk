using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
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
            Rules.Add(new CustomerRules().IsRequired(nameof(Id), Id));
            Rules.Add(new CustomerRules().IsRequired(nameof(FullName), FullName));
            Rules.Add(new CustomerRules().IsRequired(nameof(Email), Email));
            Rules.Add(new CustomerRules().IsRequired(nameof(Address.City), Address.City));
            Rules.Add(new CustomerRules().IsRequired(nameof(Address.PostCode), Address.PostCode));
            Rules.Add(new CustomerRules().IsRequired(nameof(Address.StreetName), Address.StreetName));
            Rules.Add(new CustomerRules().IsRequired(nameof(Address.HouseNumber), Address.HouseNumber));


            Rules.Add(new CustomerRules().GenerateTrackingNumber(nameof(TrackingNumber), (int)Id, out trackingNumber));
            Rules.Add(new CustomerRules().ValiditieTrackingNumber(nameof(TrackingNumber), (int)Id, TrackingNumber));

            return base.AddBusinessRules();
        }
    }
}