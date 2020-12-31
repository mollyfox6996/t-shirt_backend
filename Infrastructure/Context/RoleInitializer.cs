using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string email = configuration["AdminSettings:email"];
            string password = configuration["AdminSettings:password"];
            string dispayName = configuration["AdminSettings:displayName"];

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await userManager.FindByNameAsync(email) == null)
            {
                var admin = new AppUser { Email = email, UserName = email, DisplayName = dispayName, EmailConfirmed = true};
                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
        
    }
}