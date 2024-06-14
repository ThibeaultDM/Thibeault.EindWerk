using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules.BusinessObjects
{
    public class BO_Product : BusinessObject
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
        public List<StockAction>? StockActions { get; set; } = new();
    }
}