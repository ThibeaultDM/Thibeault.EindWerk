using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects.Enums;

namespace Thibeault.EindWerk.Objects
{
    public class OrderHeader : BaseObject
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrackingNumber { get; set; }

        public Customer Customer { get; set; }

        /// <summary>
        /// Automaticly set
        /// </summary>
        public Status Status { get; set; } = Status.New;
        //public List<OrderLineProbalyDepricated> OrderLines { get; set; } = new();        
        public List<StockAction> StockActions { get; set; } = new();

    }
}
