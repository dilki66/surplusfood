using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public static class SeedPriceStatus
    {
        public static async Task SeedAsync(DbContext dbContext)
        {
            if (dbContext.Model.FindEntityType(typeof(RefPriceStatus)) == null)
            {
                throw new InvalidOperationException("RefPriceStatus entity is not part of the dbContext model.");
            }

            var existingStatusIds = await dbContext.Set<RefPriceStatus>().Select(s => s.Id).ToListAsync();

            var priceStatusList = new List<RefPriceStatus>
            {
                    new() { Id = 1, PriceStatus = "Free" },
                    new() { Id = 2, PriceStatus = "Discount" }
            }.Where(status => !existingStatusIds.Contains(status.Id)).ToList();

            foreach (var status in priceStatusList)
            {
                dbContext.Set<RefPriceStatus>().Add(status);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
