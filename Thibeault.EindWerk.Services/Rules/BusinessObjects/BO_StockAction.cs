using Thibeault.Example.Objects.DataObjects;
using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Services.Rules.BusinessObjects
{
    public class BO_StockAction : BusinessObject
    {
        public int Id { get; set; }

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
            Rules.Add(new Rule().IsRequired(nameof(Product), Product));
            Rules.Add(new Rule().IsRequired(nameof(Action), Action));
            Rules.Add(new Rule().IsRequired(nameof(Amount), Amount));

            Rules.Add(new StockActionRules().CheckAction(nameof(Action), Product.Stock, Product.Reserved, Action, Amount));

            return base.AddBusinessRules();
        }
    }
}