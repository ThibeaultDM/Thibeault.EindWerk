using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.DataLayer.DataSeeding
{
    public class SeedDatabaseHelper
    {
        public static async Task SeedInitialUserAsync(IServiceProvider serviceProvider)
        {
            var userManger = serviceProvider.GetService<UserManager<User>>();

            var user = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userInDb = await userManger.FindByEmailAsync(user.Email);

            if (userInDb == null)
            {
                await userManger.CreateAsync(user, "Test123!");
            }
        }
    }
}
