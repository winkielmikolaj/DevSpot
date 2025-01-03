using DevSpot.Constants;
using DevSpot.Data;
using DevSpot.Models;
using DevSpot.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevSpot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //db connection from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("Database");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //adding a default user identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddScoped<IRepository<JobPosting>, JobPostingRepository>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //data seeding (Admin Role)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                //seeding new roles
                RoleSeeder.SeedRolesAsync(services).Wait();

                //seeding new users with roles
                UserSeeder.SeedUsersAsync(services).Wait();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=JobPostings}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
