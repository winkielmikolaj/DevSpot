using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateUserWithRole(userManager, "admin@devspot.com", "Admin123!", Roles.Admin);

            await CreateUserWithRole(userManager, "jobseeker@devspot.com", "JobSeeker123!", Roles.Job_Seeker);

            await CreateUserWithRole(userManager, "Employer@devspot.com", "Employer123!", Roles.Employer);

            //if (await userManager.FindByEmailAsync("admin@devspot.com") == null)
            //{
            //    var user = new IdentityUser
            //    {
            //        Email = "admin@devspot.com",
            //        EmailConfirmed = true,
            //        UserName = "admin@devspot.com"
            //    };

            //    //Admin123! is password
            //    var result = await userManager.CreateAsync(user, "Admin123!");

            //    if (result.Succeeded)
            //    {
            //        //przypisywanie roli jesli dodanie sie powiedzie
            //        await userManager.AddToRoleAsync(user, Roles.Admin);
            //    }
            //    else
            //    {
            //        throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
            //    }
        }

        //new method to refactor code above 
        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                //Admin123! is password
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    //przypisywanie roli jesli dodanie sie powiedzie
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
