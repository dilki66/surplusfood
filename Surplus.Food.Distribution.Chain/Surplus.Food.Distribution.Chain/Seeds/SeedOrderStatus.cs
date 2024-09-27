using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Seeds
{
    public static class SeedOrderStatus
    {
        public static async Task SeedAsync(DbContext dbContext)
        {
            if (dbContext.Model.FindEntityType(typeof(RefOrderStatus)) == null)
            {
                throw new InvalidOperationException("RefOrderStatus entity is not part of the dbContext model.");
            }

            var existingStatusIds = await dbContext.Set<RefOrderStatus>().Select(s => s.Id).ToListAsync();

            var priceStatusList = new List<RefOrderStatus>
            {
                    new() { Id = 1, Status = "Pending" },
                    new() { Id = 2, Status = "Accepted" },
                    new() { Id = 3, Status = "Rejected" },
                    new() { Id = 4, Status = "Completed" },
                    new() { Id = 5, Status = "Done" },
                    new() { Id = 6, Status = "Incompleted" },
                    new() { Id = 7, Status = "On the way" },
                    new() { Id = 8, Status = "Delivery Accepted" },
                    new() { Id = 9, Status = "Recieved" },
                    new() { Id = 10, Status = "Delivery Rejected" },
                    new() { Id = 11, Status = "Pending Delivery Acceptance" },
                    new() { Id = 12, Status = "Paid" }
            }.Where(status => !existingStatusIds.Contains(status.Id)).ToList();

            foreach (var status in priceStatusList)
            {
                dbContext.Set<RefOrderStatus>().Add(status);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
