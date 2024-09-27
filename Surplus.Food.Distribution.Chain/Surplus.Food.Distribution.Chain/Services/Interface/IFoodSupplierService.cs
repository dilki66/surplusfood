using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface IFoodSupplierService
    {
        Task<FoodItemListResponse> GetFoodItemsBySuplierAsync(Guid suplierId, int page);
        Task<FoodItemResponse> AddFoodItemAsync(FoodItemRequest request);
        Task<FoodItemResponse> UpdateFoodItemAsync(FoodItemRequest request, Guid id);
        Task<bool> DeleteFoodItemAsync(Guid id);
        Task<IEnumerable<FoodSupplierResponse>> GetAllSupplierDetails();
    }
}
