using Microsoft.AspNetCore.Identity;
using Surplus.Food.Distribution.Chain.Enums;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using System.Data;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public static class SeedDefasultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var guest = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@surplus.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "",
                NormalizedEmail = "admin@surplus.com".ToUpper(),
                EmailConfirmed = true,
            };

            //await userManager.CreateAsync(guest, "Super@123");
            //await userManager.AddToRoleAsync(guest, Roles.Guest.ToString());
        }
    }
}
