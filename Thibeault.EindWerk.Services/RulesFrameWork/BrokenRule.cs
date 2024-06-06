using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thibeault.EindWerk.Service
{
    public class BrokenRule
    {
        public BrokenRule(string property, string message )
        {
            PropertyName = property;
            Message = message;
        }
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
