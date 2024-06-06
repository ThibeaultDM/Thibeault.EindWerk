using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thibeault.EindWerk.Objects
{
    public class User: IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }


    }
}
