using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.EindWerk.Objects.DataObjects
{
    public class StockAction : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Enums.Action Action { get; set; }

        /// <summary>
        /// What! apples, pears!? Amount is ambiguous because products are unknown
        /// </summary>
        public int Amount { get; set; }

        public OrderHeader? OrderHeader { get; set; }

        public override string ToString()
        {
            return "Stock Action: " + Product.ToString() + ", " + Action.ToString() + " " + Amount.ToString() + ", " + OrderHeader.ToString();
        }
    }
}