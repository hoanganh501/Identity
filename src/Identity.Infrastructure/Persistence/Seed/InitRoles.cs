using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public class InitRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<string>()
                {
                    "Superadmin",
                    "Customer"
                };

                foreach (var roleName in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = roleName,
                            NormalizedName = roleName.ToUpper()
                        });
                    }
                }
            }
        }
    }
}
