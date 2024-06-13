using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects;
using Thibeault.EindWerk.Objects.Enums;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
{
    public class BO_StockAction : BusinessObject
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Automaticly set
        /// </summary>
        public Objects.Enums.Action Action { get; set; }

        /// <summary>
        /// What apples pears!? Amount is amiguos because products are unknow
        /// </summary>
        public int Amount { get; set; }

        public OrderHeader? OrderHeader { get; set; }

        public override bool AddBusinessRules()
        {

            return base.AddBusinessRules();
        }

    }

}
