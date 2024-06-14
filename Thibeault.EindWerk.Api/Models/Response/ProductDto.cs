using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Api.Models.Response
{
    public class ProductDto
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
        public List<StockActionDto> StockActions { get; set; }
        public List<BrokenRule> BrokenRules { get; set; }

        public override string ToString()
        {
            return "Product: " + SerialNumber.ToString() + " " + Name;
        }
    }
}