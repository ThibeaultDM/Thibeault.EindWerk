using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Objects.Enums;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
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
            Rules.Add(new Rule().IsRequired(nameof(Id), Id));
            Rules.Add(new Rule().IsRequired(nameof(Customer), Customer));
            Rules.Add(new Rule().IsRequired(nameof(Status), Status));
            Rules.Add(new Rule().IsRequired(nameof(StockActions), StockActions));

            Rules.Add(new ProductRules().GenerateSerialNumber(nameof(TrackingNumber), Id, out trackingNumber));
            Rules.Add(new Rule().IsRequired(nameof(TrackingNumber), TrackingNumber));

            Rules.Add(new OrderHeaderRules(mapper).CheckIfOrderIsValid(nameof(StockActions), StockActions));

            return base.AddBusinessRules();
        }

    }
}
