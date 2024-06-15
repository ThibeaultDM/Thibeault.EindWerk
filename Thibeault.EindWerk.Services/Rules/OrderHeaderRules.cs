using AutoMapper;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;
using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules
{
    public class OrderHeaderRules : Rule
    {
        private readonly IMapper mapper;

        public OrderHeaderRules(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Run through all the and give back the cumulated errors of the order in 1 message
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="stockActions"></param>
        /// <returns></returns>
        public OrderHeaderRules CheckIfOrderIsValid(string propertyName, List<StockAction> stockActions)
        {
            PropertyName = propertyName;
            string errors = null;
            try
            {
                foreach (StockAction stockAction in stockActions)
                {
                    //if (stockAction.Action != Objects.Enums.Action.Reserved)
                    //{
                    //    errors += $"|{stockAction.Product.SerialNumber}-{stockAction.Product.Name}-You can only reserve products in an order";
                    //}
                    //else
                    //{
                    BO_StockAction stockActionBo = mapper.Map<BO_StockAction>(stockAction);

                    if (!stockActionBo.IsValid)
                    {
                        errors += $"|{stockActionBo.Product.SerialNumber}-{stockActionBo.Product.Name}";

                        stockActionBo.BrokenRules.ForEach(br => errors += "-" + br.Message);

                        errors += "|";
                    }
                    //}
                }

                if (errors != null)
                {
                    throw new Exception(errors);
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