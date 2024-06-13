using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.Objects
{
    public class OrderLineProbablyDeprecated
    {
        // more in github issue
        public OrderHeader OrderHeader { get; set; }

        public StockAction StockAction { get; set; }
    }
}