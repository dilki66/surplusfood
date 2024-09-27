using Microsoft.AspNetCore.Identity;
using Surplus.Food.Distribution.Chain.Enums;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public static class SeedGuest
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var guest = new ApplicationUser
            {
                UserName = "guest",
                Email = "guest@surplus.com",
                FirstName = "Guest",
                LastName = "Guest",
                PhoneNumber = "",
                NormalizedEmail = "guest@surplus.com".ToUpper(),
                EmailConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != guest.Id))
            {
                await userManager.CreateAsync(guest, "Super@123");
                await userManager.AddToRoleAsync(guest, Roles.Guest.ToString());
            }
        }
    }
}
