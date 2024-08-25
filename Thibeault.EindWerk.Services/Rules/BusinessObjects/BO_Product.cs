using Thibeault.Example.Objects.DataObjects;
using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Services.Rules.BusinessObjects
{
    public class BO_Product : BusinessObject
    {
        private int serialNumber;
        private int stock;
        private int reserved;

        public int Id { get; set; }
        public int SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string Name { get; set; }
        public int Stock { get => stock; set => stock = value; }
        public int Reserved { get => reserved; set => reserved = value; }
        public List<StockAction> StockActions { get; set; } = new();

        public override bool AddBusinessRules()
        {
            Rules.Add(new Rule().IsRequired(nameof(Name), Name));

            Rules.Add(new ProductRules().GoThroughStockAction(nameof(SerialNumber), StockActions, out stock, out reserved));

            Rules.Add(new ProductRules().CheckStock(nameof(Stock), Stock));
            Rules.Add(new ProductRules().CheckReserved(nameof(Reserved), Reserved));
            Rules.Add(new ProductRules().CheckStockAndReservedRatio(nameof(Stock), Stock, Reserved));

            Rules.Add(new ProductRules().GenerateSerialNumber(nameof(SerialNumber), Id, out serialNumber));

            return base.AddBusinessRules();
        }
    }
}