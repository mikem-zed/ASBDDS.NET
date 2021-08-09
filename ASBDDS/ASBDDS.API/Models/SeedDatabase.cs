using ASBDDS.Shared.Models.Database.DataDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Identity;

namespace ASBDDS.API.Models
{
    public class SeedDatabase
    {
        public static async void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var dataContext = serviceProvider.GetRequiredService<DataDbContext>();
            dataContext.Database.Migrate();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            // if (!roleManager.Roles.Any(r => r.Name == "Admin"))
            // {
                var adminRole = roleManager.FindByNameAsync("Admin").Result;
                if (adminRole == null)
                {
                    adminRole = new ApplicationRole(){ Name = "Admin", NormalizedName = "ADMIN" };
                    //create the roles and seed them to the database
                    var roleResult = roleManager.CreateAsync(adminRole).Result;
                }
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userManager.FindByNameAsync("Admin");
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser()
                    {
                        UserName = "Admin", Email = "admin@asbdds.com", Name = "Admin", LastName = "Default"
                    };
                    var res = userManager.CreateAsync(adminUser, "Admin@123").Result;
                }
                var res1 = await userManager.AddToRoleAsync((ApplicationUser)adminUser, "Admin");
                if (res1.Succeeded)
                {
                    dataContext.Projects.Add(new Project()
                    {
                        Creator = adminUser,
                        Name = adminUser.Name + " " + adminUser.LastName + "'s Project"
                    });
                    dataContext.SaveChangesAsync().Wait();
                }
                // }
        }
    }
}
