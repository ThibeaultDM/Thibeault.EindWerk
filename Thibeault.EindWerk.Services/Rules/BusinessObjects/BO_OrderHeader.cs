using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Objects.Enums;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
{
    public class BO_OrderHeader : BusinessObject
    {
        public int Id { get; set; }

        public int TrackingNumber { get; set; }
        public Customer Customer { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Status Status { get; set; } = Status.New;

        public List<StockAction> StockActions { get; set; } = new();

    }
}
