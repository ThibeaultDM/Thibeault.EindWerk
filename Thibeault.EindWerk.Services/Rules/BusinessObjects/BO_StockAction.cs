using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
{
    public class BO_StockAction : BusinessObject
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Objects.Enums.Action Action { get; set; }

        /// <summary>
        /// What apples pears!? Amount is ambiguous because products are unknow
        /// </summary>
        public int Amount { get; set; }

        public OrderHeader? OrderHeader { get; set; }

        public override bool AddBusinessRules()
        {
            Rules.Add(new StockActionRules().IsRequired(nameof(Product), Product));
            Rules.Add(new StockActionRules().IsRequired(nameof(Action), Action));
            Rules.Add(new StockActionRules().IsRequired(nameof(Amount), Amount));

            Rules.Add(new StockActionRules().CheckAction(nameof(Action), Product.Stock, Product.Reserved, Action, Amount));

            return base.AddBusinessRules();
        }
    }
}