using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules
{
    public class ProductRules : Rule
    {
        /// <summary>
        /// Checks sets the stock and reserved products
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stock"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public ProductRules GoThroughStockAction(string propertyName, List<StockAction> stockActions, out int stock, out int reserved)
        {
            PropertyName = propertyName;
            stock = 0;
            reserved = 0;
            try
            {
                foreach (StockAction action in stockActions)
                {
                    if (action.Action == Objects.Enums.Action.Add)
                    {
                        stock += action.Amount;
                    }
                    else if (action.Action == Objects.Enums.Action.Remove)
                    {
                        stock -= action.Amount;
                    }
                    else
                    {
                        reserved += action.Amount;
                    }
                }
            }
            catch (Exception)
            {
                Passed = false;
                Message = "Something went wrong going through all the actions";
            }

            return this;
        }

        /// <summary>
        /// Can't reserve more than is in stock
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stock"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public ProductRules CheckStockAndReservedRatio(string propertyName, int stock, int reserved)
        {
            PropertyName = propertyName;

            try
            {
                if (stock >= reserved)
                {
                    throw new Exception("Can't reserve more than is in stock");
                }
            }
            catch (Exception ex)
            {
                Passed = false;
                Message = ex.Message;
            }

            return this;
        }

        /// <summary>
        /// Generated a serial number from the id
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="id"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public ProductRules GenerateSerialNumber(string propertyName, int id, out int serialNumber)
        {
            PropertyName = propertyName;
            // out parameter must be assign before leaving methode
            serialNumber = 0;

            try
            {
                serialNumber = id;
            }
            catch (Exception)
            {
                Passed = false;
                Message = "No valid serial number could be generated";
            }

            return this;
        }

        /// <summary>
        /// Stock can't be lower than 0
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stock"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public ProductRules CheckStock(string propertyName, int stock)
        {
            PropertyName = propertyName;

            try
            {
                if (stock > 0)
                {
                    throw new Exception("Stock can't be lower than 0");
                }
            }
            catch (Exception ex)
            {
                Passed = false;
                Message = ex.Message;
            }

            return this;
        }

        /// <summary>
        /// Can't reserve lesser than 1
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stock"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public ProductRules CheckReserved(string propertyName, int reserved)
        {
            PropertyName = propertyName;

            try
            {
                if (reserved > 0)
                {
                    throw new Exception("Can't reserve les than 0");
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