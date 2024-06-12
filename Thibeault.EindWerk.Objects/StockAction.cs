using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thibeault.EindWerk.Objects
{
    public class StockAction : BaseObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// Automaticly set
        /// </summary>
        public Enums.Action Action { get; set; }

        /// <summary>
        /// What apples pears!? Amount is amiguos because products are unknow
        /// </summary>
        public int Amount { get; set; }

        public OrderHeader? OrderHeader { get; set; }
    }
}