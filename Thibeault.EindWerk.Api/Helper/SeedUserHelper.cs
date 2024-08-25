using Microsoft.AspNetCore.Identity;

namespace Thibeault.Example.DataLayer.DataSeeding
{
    public class SeedUserHelper
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var userManger = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = new IdentityUser
            {
                UserName = "thibeault.Admin",
                Email = "thibeaultdm@hotmail.com",
                EmailConfirmed = true,
            };

            var userInDb = await userManger.FindByNameAsync(user.UserName);

            if (userInDb == null)
            {
                await userManger.CreateAsync(user, "Thibeault123!");
            }
        }
    }
}