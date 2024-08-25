using AutoMapper;
using Thibeault.Example.Objects.DataObjects;
using Thibeault.Example.Objects.Enums;
using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Services.Rules.BusinessObjects
{
    public class BO_OrderHeader : BusinessObject
    {
        public BO_OrderHeader(IMapper mapper)
        {
            this.mapper = mapper;
        }

        private int trackingNumber;
        private readonly IMapper mapper;

        public int Id { get; set; }

        public int TrackingNumber { get => trackingNumber; set => trackingNumber = value; }
        public Customer Customer { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Status Status { get; set; } = Status.New;

        public List<StockAction> StockActions { get; set; } = new();

        public override bool AddBusinessRules()
        {
            Rules.Add(new Rule().IsRequired(nameof(Customer), Customer));
            Rules.Add(new Rule().IsRequired(nameof(Status), Status));

            Rules.Add(new ProductRules().GenerateSerialNumber(nameof(TrackingNumber), Id, out trackingNumber));

            Rules.Add(new OrderHeaderRules(mapper).CheckIfOrderIsValid(nameof(StockActions), StockActions));

            return base.AddBusinessRules();
        }
    }
}