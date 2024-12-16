using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;

namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (await userManager.FindByEmailAsync("admin@devspot.com") == null)
            {
                var user = new IdentityUser
                {
                    Email = "admin@devspot.com",
                    EmailConfirmed = true,
                    UserName = "admin@devspot.com"
                };

                //Admin123! is password
                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    //przypisywanie roli jesli dodanie sie powiedzie
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
            }
        }
    }
}
