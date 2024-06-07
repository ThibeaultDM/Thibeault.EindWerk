using Thibeault.EindWerk.Services.RulesFrameWork;

namespace Thibeault.EindWerk.Services.RulesFramework.BusinessObjects
{
    public class BO_Product : BusinessObject
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Reserved { get; set; }

        /// <summary>
        /// In euro
        /// </summary>
        public double PricePerUnit { get; set; }
    }
}