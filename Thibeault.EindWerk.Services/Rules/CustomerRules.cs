using Thibeault.EindWerk.Services.Rules.RulesFramework;

namespace Thibeault.EindWerk.Services.Rules
{
    public class CustomerRules : Rule
    {
        /// <summary>
        /// Generates a trackingNumber K000 format and assigns it
        /// </summary>
        /// <param name="custemorTrackingNumber"></param>
        /// <param name="id"></param>
        /// <param name="trackingNumber"></param>
        /// <returns></returns>
        public CustomerRules GenerateTrackingNumber(string custemorTrackingNumber, int id, out string trackingNumber)
        {
            PropertyName = custemorTrackingNumber;
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

        /// <summary>
        /// Validates a trackingNumber
        /// </summary>
        /// <param name="custemorTrackingNumber"></param>
        /// <param name="id"></param>
        /// <param name="trackingNumber"></param>
        /// <returns></returns>
        public CustomerRules ValiditieTrackingNumber(string custemorTrackingNumber, int id, string trackingNumber)
        {
            PropertyName = custemorTrackingNumber;

            try
            {
                if (trackingNumber.Length == 5)
                {
                    char k = trackingNumber[0];

                    trackingNumber = trackingNumber.Remove(0, 1);

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