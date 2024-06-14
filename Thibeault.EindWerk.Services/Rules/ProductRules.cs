using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules
{
    public class ProductRules : Rule
    {
        /// <summary>
        /// Can't reserve more than is in stock
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stock"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        public ProductRules CheckStockAndReserved(string propertyName, int stock, int reserved)
        {
            PropertyName = propertyName;

            try
            {
                if (stock < reserved)
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
    }
}