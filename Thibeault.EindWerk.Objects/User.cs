using Microsoft.AspNetCore.Identity;

namespace Thibeault.EindWerk.Base
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
    }
}