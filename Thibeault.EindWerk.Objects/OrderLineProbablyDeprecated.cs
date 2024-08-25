using Thibeault.Example.Objects.DataObjects;

namespace Thibeault.Example.Objects
{
    public class OrderLineProbablyDeprecated
    {
        // more in github issue
        public OrderHeader OrderHeader { get; set; }

        public StockAction StockAction { get; set; }
    }
}