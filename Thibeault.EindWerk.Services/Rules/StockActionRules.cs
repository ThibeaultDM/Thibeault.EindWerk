using Thibeault.Example.Services.Rules.RulesFramework;

namespace Thibeault.Example.Services.Rules
{
    public class StockActionRules : Rule
    {
        /// <summary>
        /// Checks Whether the action is valid
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="product"></param>
        /// <param name="action"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public StockActionRules CheckAction(string propertyName, int productStock, int productReservations, Objects.Enums.Action action, int amount)
        {
            PropertyName = propertyName;
            int leftoverStock;

            try
            {
                if (amount < 1)
                {
                    throw new Exception("Can't do anything with an amount smaller than 1");
                }
                if (action == Objects.Enums.Action.Remove)
                {
                    leftoverStock = productStock - amount;

                    if (leftoverStock < 0)
                    {
                        throw new Exception("Can't remove more from stock then is in stock");
                    }
                }
                else if (action == Objects.Enums.Action.Reserved)
                {
                    leftoverStock = productStock - productReservations - amount;

                    if (leftoverStock < 0)
                    {
                        throw new Exception("Can't reserve more than is in stock");
                    }
                }
            }
            catch (Exception ex)
            {
                Passed = false;
                Message = ex.Message;
            }

            return this;
        }
    }
}