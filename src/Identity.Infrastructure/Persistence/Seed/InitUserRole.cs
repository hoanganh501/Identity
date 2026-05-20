using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Seed
{
    public class InitUserRole
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            var userRoles = new Dictionary<string, string>
        {
            { "admin", "Superadmin" },
            { "user", "Customer" },
            { "user1", "Customer" },
            { "user2", "Customer" }
        };

            foreach (var item in userRoles)
            {
                var username = item.Key;
                var roleName = item.Value;

                var user = await userManager.FindByNameAsync(username);

                if (user == null)
                {
                    throw new Exception($"User {username} not found");
                }

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    throw new Exception($"Role {roleName} not found");
                }

                var isInRole = await userManager.IsInRoleAsync(user, roleName);

                if (!isInRole)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
