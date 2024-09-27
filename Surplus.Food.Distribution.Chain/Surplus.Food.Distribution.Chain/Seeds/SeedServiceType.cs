using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public static class SeedServiceType
    {
        public static async Task SeedAsync(DbContext dbContext)
        {
            if (dbContext.Model.FindEntityType(typeof(RefServiceType)) == null)
            {
                throw new InvalidOperationException("RefServiceType entity is not part of the dbContext model.");
            }

            var existingServiceTypeIds = await dbContext.Set<RefServiceType>().Select(s => s.Id).ToListAsync();

            var serviceTypeList = new List<RefServiceType>
            {
                    new() { Id = 1, ServiceType = "Delivery" },
                    new() { Id = 2, ServiceType = "Self Pickup" },
                    new() { Id = 3, ServiceType = "Delivery Free" },
            }.Where(serviceType => !existingServiceTypeIds.Contains(serviceType.Id)).ToList();

            foreach (var serviceType in serviceTypeList)
            {
                dbContext.Set<RefServiceType>().Add(serviceType);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
