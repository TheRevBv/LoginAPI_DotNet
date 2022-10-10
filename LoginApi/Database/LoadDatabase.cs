using LoginApi.Models;
using Microsoft.AspNetCore.Identity;

namespace LoginApi.Database;

public class LoadDatabase
{
    private HashPassword hash = new HashPassword();
    public static async Task InsertUsers(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                Name = "Joshua Morin",
                UserName = "admin"
            };
            // await userManager.CreateAsync(user, hash.HashPassword("admin"));
            await userManager.CreateAsync(user, "TheRev417$");
        }
    }
}
