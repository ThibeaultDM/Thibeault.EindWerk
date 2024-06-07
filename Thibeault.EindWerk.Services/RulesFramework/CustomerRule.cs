using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Services.RulesFrameWork;

namespace Thibeault.EindWerk.Services.RulesFramework
{
    public class CustomerRule : Rule
    {
        public CustomerRule GenerateTrackingNumber(string propertyCustemorId, int id, out string trackingNumber)
        {
            this.PropertyName = propertyCustemorId;
            trackingNumber = "K";

            try
            {
                int trackingId = id + 999;

                if (trackingId > 10000 || trackingId < 999)
                {
                    throw new Exception();
                }

                trackingNumber += trackingId.ToString();

            }
            catch (Exception)
            {
                this.Passed = false;
                this.Message = "No valid trackingNumber was generated";
            }

            return this;
        }

        public CustomerRule ValiditieTrackingNumber(string propertyCustomerId, string trackingNumber)
        {
            this.PropertyName = propertyCustomerId;

            try
            {
                if (trackingNumber.Length == 5)
                {
                    char k = trackingNumber[0];

                    trackingNumber = trackingNumber.Remove(0);

                    int number = Convert.ToInt16(trackingNumber);

                    if (k == 'K' && 10000 > number && number > 999)
                    {
                        return this;
                    }

                    else
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

                throw;
            }
        }
    }
}
