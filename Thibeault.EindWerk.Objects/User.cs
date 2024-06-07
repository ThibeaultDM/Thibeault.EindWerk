using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Thibeault.EindWerk.Objects
{
    public class User : IdentityUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get => base.Id; set => base.Id = value; }
    }
}