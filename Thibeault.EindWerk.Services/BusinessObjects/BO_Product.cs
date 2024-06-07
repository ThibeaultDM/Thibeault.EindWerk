using Thibeault.EindWerk.Services.RulesFrameWork;

namespace Thibeault.EindWerk.Services.BusinessObjects
{
    public class BO_Product : BusinessObject
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }
    }
}