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
        public CustomerRule GenerateId(string propertyCustemorId, out string Id) 
        {
            this.PropertyName = propertyCustemorId;
            // write it to database and get it's key
            return this;
        }
    }
}
