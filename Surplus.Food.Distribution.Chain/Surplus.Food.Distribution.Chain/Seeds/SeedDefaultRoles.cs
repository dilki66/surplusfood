using Microsoft.AspNetCore.Identity;
using Surplus.Food.Distribution.Chain.Enums;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public class SeedDefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.Admin.ToString(),
                    NormalizedName = "ADMIN",
                    CreatedDate = DateTime.Now
                });
            }

            if (!await roleManager.RoleExistsAsync(Roles.Customer.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.Customer.ToString(),
                    NormalizedName = "CUSTOMER",
                    CreatedDate = DateTime.Now
                });
            }

            if (!await roleManager.RoleExistsAsync(Roles.Donor.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.Donor.ToString(),
                    NormalizedName = "DONOR",
                    CreatedDate = DateTime.Now
                });
            }

            if (!await roleManager.RoleExistsAsync(Roles.FoodSupplier.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.FoodSupplier.ToString(),
                    NormalizedName = "FOODSUPPLIER",
                    CreatedDate = DateTime.Now
                });
            }

            if (!await roleManager.RoleExistsAsync(Roles.DeliveryPerson.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.DeliveryPerson.ToString(),
                    NormalizedName = "DELIVERYPERSON",
                    CreatedDate = DateTime.Now
                });
            }

            if (!await roleManager.RoleExistsAsync(Roles.Guest.ToString()))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Roles.Guest.ToString(),
                    NormalizedName = "GUEST",
                    CreatedDate = DateTime.Now
                });
            }
        }
    }
}
