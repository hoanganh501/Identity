using Domain.Entity;
using Microsoft.AspNetCore.Identity;

public class InitUser
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager)
    {
        var userList = new List<string>
        {
            "admin",
            "user",
            "user1",
            "user2"
        };

        foreach (var user in userList)
        {
            var existing = await userManager.FindByNameAsync(user);

            if (existing == null)
            {
                var users = new ApplicationUser
                {
                    UserName = user,
                    NormalizedUserName = user.ToUpperInvariant()
                };

                var result = await userManager.CreateAsync(users, "P@ssword123");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}