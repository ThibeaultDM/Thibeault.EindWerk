using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules
{
    public class CustomerRules : Rule
    {
        public CustomerRules GenerateTrackingNumber(string propertyCustemorId, int id, out string trackingNumber)
        {
            PropertyName = propertyCustemorId;
            trackingNumber = "K";

            try
            {
                if (id > 9999 || id < 1)
                {
                    throw new Exception();
                }

                // change id to a 4 digit format and pad it with 0's ( my spelling test results :') )
                trackingNumber += id.ToString().PadLeft(4, '0');
            }
            catch (Exception)
            {
                Passed = false;
                Message = "No valid trackingNumber was generated";
            }

            return this;
        }

        public CustomerRules ValiditieTrackingNumber(string propertyCustomerId, int id, string trackingNumber)
        {
            PropertyName = propertyCustomerId;

            try
            {
                if (trackingNumber.Length == 5)
                {
                    char k = trackingNumber[0];

                    trackingNumber = trackingNumber.Remove(0,1);

                    int number = Convert.ToInt16(trackingNumber);

                    if (k != 'K' || 9999 < id || id < 1 || number != id)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Passed = false;
                Message = "No valid trackingNumber was generated";
            }

            return this;
        }
    }
}