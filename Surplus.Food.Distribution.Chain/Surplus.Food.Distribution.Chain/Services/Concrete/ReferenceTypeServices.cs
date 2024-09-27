using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class ReferenceTypeServices(ApplicationDbContext context, IMemoryCache memoryCache) : IReferenceTypeService
    {
        #region Private methods
        private async Task<T> GetCachedReferenceDataAsync<T>(string cacheKey, Func<Task<T>> acquire)
        {
            if (memoryCache.TryGetValue(cacheKey, out T cachedData))
            {
                return cachedData;
            }

            var data = await acquire();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            };

            memoryCache.Set(cacheKey, data, cacheEntryOptions);

            return data;
        }

        #endregion

        public async Task<RefResponse<RefOrderStatus>> GetOrderStatusAsync()
        {
            return await GetCachedReferenceDataAsync("OrderStatusList", async () =>
            {
                var orderStatus = await context.RefOrderStatuses.ToListAsync();
                return new RefResponse<RefOrderStatus>
                {
                    ReferenceList = orderStatus
                };
            });
        }

        public async Task<RefResponse<RefPriceStatus>> GetPriceStatusAsync()
        {
            return await GetCachedReferenceDataAsync("PriceStatusList", async () =>
            {
                var priceStatus = await context.RefPriceStatuses.ToListAsync();
                return new RefResponse<RefPriceStatus>
                {
                    ReferenceList = priceStatus
                };
            });
        }

        public async Task<RefResponse<RefServiceType>> GetServiceTypeAsync()
        {
            return await GetCachedReferenceDataAsync("ServiceTypeList", async () =>
            {
                var serviceType = await context.RefServiceTypes.ToListAsync();
                return new RefResponse<RefServiceType>
                {
                    ReferenceList = serviceType
                };
            });
        }
    }
}
