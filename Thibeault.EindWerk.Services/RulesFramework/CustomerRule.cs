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

                trackingNumber += trackingId.ToString();

            }
            catch (Exception)
            {
                this.Passed = false;
                this.Message = "No Id was given to create trackingNumber";
            }

            return this;
        }
    }
}
