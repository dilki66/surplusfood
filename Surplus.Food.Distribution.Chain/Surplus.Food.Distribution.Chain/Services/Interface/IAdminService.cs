using Surplus.Food.Distribution.Chain.Models.RequestModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface IAdminService
    {
        Task<bool> UpdateFoodSupplier(FoodSuplierRequest request, Guid id);
        Task<bool> DesableFoodSupplier(Guid id);
        Task<bool> DisableCustomer(Guid id);
        Task<bool> DisableDonor(Guid id);
    }
}
