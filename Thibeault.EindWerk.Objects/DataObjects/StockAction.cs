using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.Example.Objects.DataObjects
{
    public class StockAction : BaseObject
    {
        // todo een lijn die niet gekoppeld is aan een bestelling kan evenmin
        public StockAction()
        {
        }

        public StockAction(Product product, Enums.Action action, int amount) : base()
        {
            Product = product;
            Action = action;
            Amount = amount;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Automatically set
        /// </summary>
        public Enums.Action Action { get; set; } = Enums.Action.Reserved;

        /// <summary>
        /// What! apples, pears!? Amount is ambiguous because products are unknown
        /// </summary>
        public int Amount { get; set; }

        public OrderHeader? OrderHeader { get; set; }

        //public override string ToString()
        //{
        //    string Tostring = "Stock Action: " + Product.ToString() + ", " + Action.ToString() + " " + Amount.ToString();

        //    if (OrderHeader != null)
        //    {
        //        Tostring += "\n" + OrderHeader.ToString();
        //    }
        //    return Tostring;
        //}
    }
}