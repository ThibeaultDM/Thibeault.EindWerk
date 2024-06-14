using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thibeault.EindWerk.Objects.Enums;

namespace Thibeault.EindWerk.Objects.DataObjects
{
    public class OrderHeader : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackingNumber { get; set; }

        public Customer Customer { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Status Status { get; set; } = Status.New;

        //public List<OrderLineProbablyDeprecated> OrderLines { get; set; } = new();
        public List<StockAction> StockActions { get; set; } = new();

        public override string ToString()
        {
            return "OrderHeader: " + TrackingNumber.ToString() + "\n" + Customer.ToString() + "\n" + Status.ToString();
        }
    }
}