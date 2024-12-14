using DevSpot.Constants;
using Microsoft.AspNetCore.Identity;

namespace DevSpot.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //checking if admin is already added in database
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            //checking if JobSeeker is already added in database
            if (!await roleManager.RoleExistsAsync(Roles.Job_Seeker))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Job_Seeker));
            }

            //checking if Employer is already added in database
            if (!await roleManager.RoleExistsAsync(Roles.Employer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employer));
            }

        }
    }
}
