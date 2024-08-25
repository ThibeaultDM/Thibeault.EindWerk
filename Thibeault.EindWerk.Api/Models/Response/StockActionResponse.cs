using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Api.Models.Response
{
    public class StockActionResponse
    {
        public ProductResponseForStockAction Product { get; set; }
        public Objects.Enums.Action Action { get; set; }
        public int Amount { get; set; }
        public List<BrokenRule> BrokenRules { get; set; }
    }
}