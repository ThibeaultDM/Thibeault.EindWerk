using Thibeault.Example.Api.Models.Input;
using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Api.Models.Response
{
    public class ProductDto
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
        public List<StockActionDto> StockActions { get; set; }
        public List<BrokenRule> BrokenRules { get; set; }
    }
}